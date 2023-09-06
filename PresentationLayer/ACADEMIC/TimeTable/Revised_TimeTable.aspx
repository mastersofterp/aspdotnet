<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Revised_TimeTable.aspx.cs" Inherits="ACADEMIC_TIMETABLE_Revised_TimeTable" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../plugins/touch-dnd.js"></script>--%>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("http://cdn.jsdelivr.net/json2/0.1/json2.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/touch-dnd.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            bindDataTable();
            bindDataTable1();//for jQuery  forcefully work after post back
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable1);

        });
    </script>


    <style>
        #rhs select.form-control {
            display: none !important;
        }
    </style>



    <%-- <script>
      function test(dayno) {
          //debugger;
          var $value = dayno;
          $('#tblslots thead').find('th').each(function () {
              var xx = $(this).attr('itemid');

              if (xx == $value) {
                  $(this).parents('thead').siblings('tbody').find('tr').each(function () {
                      $(this).find('td').eq(xx).addClass('bg-danger droppable');
                      $(this).find('td').eq(xx).siblings('td').removeClass('bg-danger droppable');
                  })
              } else { }
          });
      }

      function test1() {
          $('#tblslots thead').find('th').each(function () {
              var xx = $(this).attr('itemid');

              $(this).parents('thead').siblings('tbody').find('tr').each(function () {

                  if ($(this).find('td').hasClass('droppable')) {
                      $(this).find('td').removeClass('bg-danger droppable');
                  } else { }
              });
          });
      }

      var parameter = Sys.WebForms.PageRequestManager.getInstance();
      parameter.add_endRequest(function () {
          function test(dayno) {
              //debugger;
              var $value = dayno;
              $('#tblslots thead').find('th').each(function () {
                  var xx = $(this).attr('itemid');

                  if (xx == $value) {
                      $(this).parents('thead').siblings('tbody').find('tr').each(function () {
                          $(this).find('td').eq(xx).addClass('bg-danger droppable');
                          $(this).find('td').eq(xx).siblings('td').removeClass('bg-danger droppable');
                      })
                  } else { }
              });
          }


          function test1() {
              $('#tblslots thead').find('th').each(function () {
                  var xx = $(this).attr('itemid');

                  $(this).parents('thead').siblings('tbody').find('tr').each(function () {

                      if ($(this).find('td').hasClass('droppable')) {
                          $(this).find('td').removeClass('bg-danger droppable');
                      } else { }
                  });
              });
          }
      });
    </script>--%>

    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }

        /*.text-primary {
            background-color: violet;
        }*/

        .container {
            width: 90%;
        }

        #tblslots li {
            list-style-type: none;
        }
    </style>


    <style>
        #jquery-script-menu {
            position: fixed;
            height: 90px;
            width: 100%;
            top: 0;
            left: 0;
            border-top: 5px solid #316594;
            background: #fff;
            -moz-box-shadow: 0 2px 3px 0px rgba(0, 0, 0, 0.16);
            -webkit-box-shadow: 0 2px 3px 0px rgba(0, 0, 0, 0.16);
            box-shadow: 0 2px 3px 0px rgba(0, 0, 0, 0.16);
            z-index: 999999;
            padding: 10px 0;
            -webkit-box-sizing: content-box;
            -moz-box-sizing: content-box;
            box-sizing: content-box;
        }

        .jquery-script-center {
            width: 960px;
            margin: 0 auto;
        }

            .jquery-script-center ul {
                width: 212px;
                float: left;
                line-height: 45px;
                margin: 0;
                padding: 0;
                list-style: none;
            }

            .jquery-script-center a {
                text-decoration: none;
            }

        .jquery-script-ads {
            width: 728px;
            height: 90px;
            float: right;
        }

        .jquery-script-clear {
            clear: both;
            height: 0;
        }
    </style>

    <style>
        .draggable, .droppable {
            border: 1px dashed #000;
            min-width: 140px;
            min-height: 40px;
            height: auto;
            width: auto;
            /*  display: inline-block;*/
            box-sizing: border-box;
            vertical-align: top;
            cursor: move;
        }

        .droppable {
            border-color: #232923;
            padding: 5px 2px;
            height: auto;
        }


            .droppable > div {
                display: inline-block;
                vertical-align: top;
                margin: auto 5px;
                padding: 2px 6px;
                background-color: #eee;
                position: relative;
                border-radius: 8px;
            }

                .droppable > div::before {
                    cursor: pointer;
                    font-size: 12px;
                }

                .droppable > div:after {
                    content: ',';
                    display: inline-block;
                }

                .droppable > div:last-child:after {
                    display: none;
                }

        .draggable.clone {
            background-color: #abe2a5;
        }

        .droppable.big {
            height: 60px;
            width: 100px;
        }

            .droppable.big .droppable {
                float: right;
            }

        .droppable.active {
            background-color: #abe2a5;
        }

        .droppable.drop-here {
            background-color: #3498db;
        }

        .droppable .draggable {
            width: 16px;
            height: auto;
            margin: 0 3px;
            font-size: 10px;
            padding: 2px;
            background-color: aliceblue;
        }

        ul.list, ul.grid {
            list-style-type: none;
            padding: 0;
            /*  width: 162px;*/
        }

            ul.grid.wide {
                width: 100%;
                height: 50px;
            }

            ul.grid.active {
                background-color: #abe2a5;
            }

            ul.list li, ul.grid li {
                border: 1px solid #DCDBDD;
                margin: 2px;
                padding: 2px 10px;
                cursor: pointer;
                background-color: #F6F5F7;
                box-sizing: border-box;
                vertical-align: top;
            }

            ul.list li {
                height: auto;
                display: inline-block;
                vertical-align: top;
                position: relative;
                cursor: move;
                border-radius: 8px;
            }

            ul.grid li {
                float: left;
                height: 50px;
                width: 50px;
            }

                ul.list li.placeholder, ul.grid li.placeholder {
                    border-style: dashed;
                    background-color: transparent;
                }




        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 2px 2px 15px 2px;
        }

        .dropb {
            display: inline-block;
            vertical-align: 5px;
            margin: auto 5px;
            padding: 2px 6px;
            background-color: #eee;
            position: relative;
            border-radius: 8px;
        }

        .dropc {
            background-color: white;
        }

            .dropc li {
                list-style-type: none;
            }

        body {
            background-color: #fafafa;
            font-family: 'Roboto', sans-serif;
        }
    </style>

    <style>
        #tblslots .form-control {
            padding: 0rem 0.75rem !important;
            height: 20px !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server"
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

    <asp:UpdatePanel ID="updTimeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchoolInstitute" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" TabIndex="2" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true">Department</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" TabIndex="3" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select ddlDepartment" InitialValue="0" ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"> </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" TabIndex="4" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true">Programme/ Branch  </asp:Label>
                                        </div>

                                        <asp:DropDownList ID="ddlBranch" TabIndex="5" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Programme  / Branch" ValidationGroup="course"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label runat="server" ID="lblDYddlScheme" Font-Bold="true">Scheme  </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" TabIndex="6" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="course"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"> </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" TabIndex="7" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"> </asp:Label>
                                        </div>

                                        <asp:DropDownList ID="ddlSection" runat="server" TabIndex="8" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSlotType" runat="server" Font-Bold="true">Slot Type </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSlotType" runat="server" TabIndex="9" AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSlotType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Existing Dates </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExistingDates" runat="server" TabIndex="10" AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlExistingDates_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <%-- <div class="form-group col-md-3">
                                <label for="city">Revised No. </label>
                                <asp:DropDownList ID="ddlRevisedNo" runat="server" AppendDataBoundItems="True"
                                    OnSelectedIndexChanged="ddlRevisedNo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Start Date  </label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgStartDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" data-inputmask="'alias': 'dd/mm/yyyy'"
                                                AutoPostBack="true" OnTextChanged="txtStartDate_TextChanged" data-mask="" Style="z-index: 0" TabIndex="11" />
                                            <%-- onchange="test()" --%>
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please Enter Start Date">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtStartDate" PopupButtonID="imgStartDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" TargetControlID="txtStartDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" EmptyValueMessage="Please Enter Date"
                                                ControlExtender="meeStartDate" ControlToValidate="txtStartDate" IsValidEmpty="true"
                                                InvalidValueMessage="Start Date is Invalid!" Display="None" TooltipMessage="Input a Start Date"
                                                ErrorMessage="Please Enter Start Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                SetFocusOnError="true" />
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>End Date </label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgEndDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" data-inputmask="'alias': 'dd/mm/yyyy'"
                                                AutoPostBack="true" OnTextChanged="txtEndDate_TextChanged" data-mask="" Style="z-index: 0" TabIndex="12" />
                                            <%-- onchange="test()" --%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEndDate"
                                                Display="None" ErrorMessage="Please Enter End Date" Visible="false">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtEndDate" PopupButtonID="imgEndDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" TargetControlID="txtEndDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter End Date" Visible="false"
                                                ControlExtender="meeEndDate" ControlToValidate="txtEndDate" IsValidEmpty="true"
                                                InvalidValueMessage="End Date is Invalid!" Display="None" TooltipMessage="Input a End Date"
                                                ErrorMessage="Please Enter End Date." EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                SetFocusOnError="true" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remark </label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TabIndex="12" TextMode="MultiLine" MaxLength="128" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDateDetails" runat="server" visible="false">
                                        <br />
                                        <p class="text-center" style="border-style: double; font-size: 14px; font-weight: bold; color: #3c8dbc;">
                                            <asp:Label ID="lblTitleDate" runat="server" Text="Selected Session Start & End Date :"></asp:Label>
                                        </p>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">$</span><span><span style="color: green; font-weight: bold">  Indicates main teacher. </span></span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">#</span><span><span style="color: green; font-weight: bold">  Indicates additional teacher of subject. </span></span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">[ T ]</span><span><span style="color: green; font-weight: bold"> Indicates Tutorial. </span></span></p>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="course" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <input id="btnSave" type="button" title="submit" value="Save" class="btn btn-primary" tabindex="13" />
                                        <asp:Button ID="btnLock" runat="server" Visible="False"
                                            OnClick="btnLock_Click" OnClientClick="return confirm ('Do you want to lock the time table? Once Locked it cannot be modified or changed!');" Text="Lock" CssClass="btn btn-primary" TabIndex="14" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-warning" OnClientClick="return true;" OnClick="btnClear_Click" TabIndex="15" />
                                    </div>

                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-md-2" runat="server" id="divCourses" visible="false">
                                        <ul class="list" id="rhs">
                                            <div id="Div2" class="sub-heading" runat="server">
                                                <h5>Courses</h5>
                                            </div>
                                            <input type="text" id="search_course_input" onkeyup="myFunction()" placeholder="Search" class="form-control">
                                            &nbsp;<div class="" style="height: 495px; overflow: scroll;" id="search_course">
                                                <asp:ListView ID="dListFaculty" runat="server" OnDataBound="dListFaculty_DataBound">
                                                    <ItemTemplate>
                                                        <%--Do not Remove Classes and AlternateText CTNO--%>
                                                        <div class="draggable MainClass SaveData" id="drg" runat="server">
                                                            <asp:ImageButton ID="btn" runat="server" src="../../images/delete.gif" Style="cursor: pointer; font-size: 3px; display: none;" ImageAlign="Left" OnClientClick="return removediv1(this);"></asp:ImageButton>
                                                            <asp:Label ID="lblFac" CssClass="search_text flotValue remove" runat="server" Text='<%# Eval("FACULTY") %>' ToolTip='<%#Eval("UA_FULLNAME")%>' AlternateText='<%#Eval("CT_NO")%>'></asp:Label>
                                                            <asp:DropDownList ID="ddlRoom" runat="server" CssClass="class_ss" AppendDataBoundItems="true" onchange="MyDropDownList_OnChange(this)"></asp:DropDownList>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </ul>
                                        <div class="bag">
                                        </div>
                                    </div>

                                    <div class="col-md-10">
                                        <asp:Panel ID="pnlSlots" Visible="true" runat="server">
                                            <asp:ListView ID="lvSlots" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Time Table Slots </h5>
                                                    </div>
                                                    <div class="table-responsive">
                                                        <table id="tblslots" class="table table-bordered table-hover table-striped">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Time</th>
                                                                    <th id="Mon" itemid="1">Monday</th>
                                                                    <th id="Tue" itemid="2">Tuesday</th>
                                                                    <th id="Wed" itemid="3">Wednesday</th>
                                                                    <th id="Thu" itemid="4">Thursday</th>
                                                                    <th id="Fri" itemid="5">Friday</th>
                                                                    <th id="Sat" itemid="6">Saturday</th>
                                                                    <th id="Sun" itemid="7">Sunday</th>
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
                                                            <asp:Label ID="lbltime" CssClass="SlotNo" runat="server" Text='<%# Eval("SLOTTIME") %>' ToolTip='<%#Eval("SLOTNO")%>' AlternateText='<%#Eval("SLOTNO")%>' />
                                                        </td>

                                                        <td id="drp1" class="droppable" runat="server">
                                                            <asp:HiddenField ID="hdnMon" runat="server" Value='<%# Bind("SLOTNO") %>' />
                                                            <div title="1" style="display: none"></div>
                                                            <%--Do not Remove this Div--%>
                                                        </td>

                                                        <td id="drp2" class="droppable" runat="server">
                                                            <asp:HiddenField ID="hdnTue" runat="server" Value='<%# Bind("SLOTNO") %>' />
                                                            <div title="2" style="display: none"></div>
                                                            <%--Do not Remove this Div--%>
                                                        </td>

                                                        <td id="drp3" class="droppable" runat="server"><%--ondblclick="(function(_this){_this.parentNode.removeChild(_this);})(this);"--%>
                                                            <asp:HiddenField ID="hdnWed" runat="server" Value='<%# Bind("SLOTNO") %>' />
                                                            <div title="3" style="display: none"></div>
                                                            <%--Do not Remove this Div--%>
                                                        </td>

                                                        <td id="drp4" class="droppable" runat="server"><%--ondblclick="(function(_this){_this.parentNode.removeChild(_this);})(this);"--%>
                                                            <asp:HiddenField ID="hdnThurs" runat="server" Value='<%# Bind("SLOTNO") %>' />
                                                            <div title="4" style="display: none"></div>
                                                            <%--Do not Remove this Div--%>
                                                        </td>

                                                        <td id="drp5" class="droppable" runat="server">
                                                            <asp:HiddenField ID="hdnFri" runat="server" Value='<%# Bind("SLOTNO") %>' />
                                                            <div title="5" style="display: none"></div>
                                                            <%--Do not Remove this Div--%>
                                                        </td>

                                                        <td id="drp6" class="droppable" runat="server">
                                                            <asp:HiddenField ID="hdnSat" runat="server" Value='<%# Bind("SLOTNO") %>' />
                                                            <div title="6" style="display: none"></div>
                                                            <%--Do not Remove this Div--%>
                                                        </td>

                                                        <td id="drp7" class="droppable" runat="server">
                                                            <asp:HiddenField ID="hdnSun" runat="server" Value='<%# Bind("SLOTNO") %>' />
                                                            <div title="7" style="display: none"></div>
                                                            <%--Do not Remove this Div--%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <!-- Modal Popup Clash -->
                        <div class="modal fade" id="ModelPopUp" role="dialog">
                            <asp:UpdatePanel ID="lblPopup" runat="server">
                                <ContentTemplate>
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <!-- Modal Header -->
                                            <div class="modal-header">
                                                <button type="button" id="btnok" class="btn btn-primary">Ok</button>
                                                <button type="button" id="btnCancel" class="btn btn-danger">Remove</button>
                                            </div>

                                            <!-- Modal body -->
                                            <div class="modal-body text-center">
                                                <asp:Label ID="lbluserMsg" runat="server" Visible="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>



                        <asp:HiddenField ID="hdnRoomMandate" runat="server" />
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnLock" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <%--<script type="text/javascript">
                    RunThisAfterEachAsyncPostback();
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
                </script>--%>

    <script>
        function myFunction() {
            // Declare variables
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById('search_course_input');
            filter = input.value.toUpperCase();
            ul = document.getElementById("search_course");
            li = ul.querySelectorAll('div .search_text');
            console.log(li)
            li.forEach(list => {
                //alert(list);
                if (list.textContent.toUpperCase().includes(filter)) {
                    list.closest('div').style.display = 'block';
        }
        else {
                    list.closest('div').style.display = 'none';
        }
        });
        }
    </script>

    <script type="text/javascript" language="javascript">
        function RunThisAfterEachAsyncPostback() {
            totAllSubjects(headchk);
        }


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
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        if (headchk.id == e.id) {
                            alert(headchk.id)
                            //                                         $(headchk).css("background-color", "black");
                            headchk.style.backgroundColor = '#284775';
                        }
                        else
                            if (headchk.id == e.id)
                                headchk.backgroundColor = '#339900'

                }
            }
        }
        $(':checkbox').change(function () {

            $(this).css('background-color', this.checked ? 'red' : 'transparent');
        });

    </script>

    <script type="text/javascript">
       
        function bindDataTable() {
            var myDT = $(function () {
                $('#lhs').sortable({ connectWith: '.droppable' })
                    //$('#rhs').sortable({ connectWith: '#lhs' })
                    .on('sortable:receive', function (e, ui) {
                        ui.item.removeClass('draggable')
                    })
                $('.draggable').draggable({ connectWith: '#rhs', clone: true })
                $('.droppable').droppable({ activeClass: 'active', })

                $('.droppable').sortable({ connectWith: '#rhs' })

                // Added By Sarang 19/11/2022 Start

                $('.draggable').draggable().on('draggable:stop', function (e, ui) {
                    // debugger;
                    $(this).addClass("text-success").find("input").css({ "display": "none" });
                    var x = 0;
                    var ctno = $(this).find("span").attr("AlternateText");
                    //console.log(this)
                    var slot = $('.flotValue').closest("tr").find("[id*=lbltime]").attr("AlternateText");
                    var roomno = $('.flotValue').closest("td").find("select").val();
                    var dayno = $('.flotValue').closest("td").find("div").attr("title");
                    //$('.flotValue').closest("td").find("div").draggable( "destroy" );
                    $('.droppable').find("span").removeClass("flotValue");


                    if (typeof (slot) != "undefined") {
                        $.ajax({
                            url: '<%=Page.ResolveUrl("~/ACADEMIC/TimeTable/Revised_TimeTable.aspx/Get_Dynamic_Controls")%>',
                            data: '{Sessionno:' + $(<%=ddlSession.ClientID%>).val() + ', Collegeid:' + $(<%=ddlSchoolInstitute.ClientID%>).val() + ', Sectionno:' + $(<%=ddlSection.ClientID%>).val() + ', Slotno:' + slot + ', ctno:' + ctno + ', dayno:' + dayno + ', startdate:' + JSON.stringify($(<%=txtStartDate.ClientID%>).val()) + ', enddate:' + JSON.stringify($(<%=txtEndDate.ClientID%>).val()) + ', roomno:' + roomno + '}',
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                var data = JSON.stringify(r.d);
                                var x = JSON.parse(data);
                                var count = 0;
                                if (x != "0") {
                                    $('#ModelPopUp').modal('show');
                                    $('#ModelPopUp').fadeIn();
                                    document.getElementById('<%=lbluserMsg.ClientID%>').innerText = x;

                                    //if (document.getElementById('<%=lbluserMsg.ClientID%>').innerText != "0") {
                                    $("#btnok").on("click", function () {
                                        $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                                        $('.droppable').closest("td").find('div').removeClass("MainClass");
                                        $('.droppable').closest("td").find('span').removeClass("remove");
                                        $('#ModelPopUp').modal('hide');
                                        $('#ModelPopUp').fadeOut();
                                        count++;
                                    });

                                    $("#btnCancel").click(function () {
                                        if (count == 0) {
                                            if (confirm('Are you sure you want to delete this Faculty') == true) {
                                                var id = $('.droppable').find('.MainClass').attr("id");
                                                   
                                                //var id = $('.droppable').find('.remove').closest("li").attr("id");
                                                $('.droppable').find(".MainClass").remove();
                                                $('.droppable').closest("td").find('span').removeClass("remove");
                                                $('.droppable').closest("td").find('div').removeClass("MainClass");
                                                $('#ModelPopUp').modal('hide');
                                                $('#ModelPopUp').fadeOut();
                                                count++;
                                            }
                                            else {
                                                $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                                                $('.droppable').closest("td").find('span').removeClass("remove");
                                                $('.droppable').closest("td").find('div').removeClass("MainClass");
                                                $('#ModelPopUp').modal('hide');
                                                $('#ModelPopUp').fadeOut();
                                                count++;
                                            }
                                        }
                                    });
                                    //}
                                    //else {

                                    //}
                                }
                                else {
                                    //alert('hii')
                                    $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                                    $('.droppable').closest("td").find('span').removeClass("remove");
                                    $('.droppable').closest("td").find('div').removeClass("MainClass");
                                    $('#ModelPopUp').modal('hide');
                                    $('#ModelPopUp').fadeOut();
                                }
                            },
                            error: function (e) {
                                console.log(e);
                            }
                        });
                    }
                    else {
                        $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                        $('.droppable').closest("td").find('span').removeClass("remove");
                        //$('.droppable').closest("td").find('div').removeClass("MainClass");
                        $('#ModelPopUp').modal('hide');
                        $('#ModelPopUp').fadeOut();

                    }
                })


                $('.draggable').draggable().on('draggable:start', function (e, ui) {
                    $(this).addClass("text-success").find("span").css({ "color": "black" });
                    $(this).addClass("text-success").find("input").css({ "display": "block" });
                    $(this).addClass("text-success").css({ "border-left": "1px solid #2196F3", "background-color": "#a1daf6" });
                    $(this).find('input[type=hidden]').attr("type", "text");


                })
            })
        }

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {

                bindDataTable();
                //bindDataTable1();//for jQuery forcefully work after post back
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
                //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable1);

            });
            function bindDataTable() {
                var myDT = $(function () {
                    $('#lhs').sortable({ connectWith: '.droppable' })
                        //$('#rhs').sortable({ connectWith: '#lhs' })
                        .on('sortable:receive', function (e, ui) {
                            ui.item.removeClass('draggable')
                        })
                    $('.draggable').draggable({ connectWith: '#rhs', clone: true })
                    $('.droppable').droppable({ activeClass: 'active', })

                    $('.droppable').sortable({ connectWith: '#rhs' })

                    // Added By Sarang 19/11/2022 Start

                    $('.draggable').draggable().on('draggable:stop', function (e, ui) {
                        // debugger;
                        $(this).addClass("text-success").find("input").css({ "display": "none" });
                        var x = 0;
                        var ctno = $(this).find("span").attr("AlternateText");
                        //console.log(this)
                        var slot = $('.flotValue').closest("tr").find("[id*=lbltime]").attr("AlternateText");
                        var roomno = $('.flotValue').closest("td").find("select").val();
                        var dayno = $('.flotValue').closest("td").find("div").attr("title");
                        //$('.flotValue').closest("td").find("div").draggable( "destroy" );
                        $('.droppable').find("span").removeClass("flotValue");


                        if (typeof (slot) != "undefined") {
                            $.ajax({
                                url: '<%=Page.ResolveUrl("~/ACADEMIC/TimeTable/Revised_TimeTable.aspx/Get_Dynamic_Controls")%>',
                                data: '{Sessionno:' + $(<%=ddlSession.ClientID%>).val() + ', Collegeid:' + $(<%=ddlSchoolInstitute.ClientID%>).val() + ', Sectionno:' + $(<%=ddlSection.ClientID%>).val() + ', Slotno:' + slot + ', ctno:' + ctno + ', dayno:' + dayno + ', startdate:' + JSON.stringify($(<%=txtStartDate.ClientID%>).val()) + ', enddate:' + JSON.stringify($(<%=txtEndDate.ClientID%>).val()) + ', roomno:' + roomno + '}',
                                type: 'POST',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (r) {
                                    var data = JSON.stringify(r.d);
                                    var x = JSON.parse(data);
                                    var count = 0;
                                    if (x != "0") {
                                        $('#ModelPopUp').modal('show');
                                        $('#ModelPopUp').fadeIn();
                                        document.getElementById('<%=lbluserMsg.ClientID%>').innerText = x;

                                        //if (document.getElementById('<%=lbluserMsg.ClientID%>').innerText != "0") {
                                        $("#btnok").on("click", function () {
                                            $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                                            $('.droppable').closest("td").find('div').removeClass("MainClass");
                                            $('.droppable').closest("td").find('span').removeClass("remove");
                                            $('#ModelPopUp').modal('hide');
                                            $('#ModelPopUp').fadeOut();
                                            count++;
                                        });

                                        $("#btnCancel").click(function () {
                                            if (count == 0) {
                                                if (confirm('Are you sure you want to delete this Faculty') == true) {
                                                    var id = $('.droppable').find('.MainClass').attr("id");
                                                   
                                                    //var id = $('.droppable').find('.remove').closest("li").attr("id");
                                                    $('.droppable').find(".MainClass").remove();
                                                    $('.droppable').closest("td").find('span').removeClass("remove");
                                                    $('.droppable').closest("td").find('div').removeClass("MainClass");
                                                    $('#ModelPopUp').modal('hide');
                                                    $('#ModelPopUp').fadeOut();
                                                    count++;
                                                }
                                                else {
                                                    $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                                                    $('.droppable').closest("td").find('span').removeClass("remove");
                                                    $('.droppable').closest("td").find('div').removeClass("MainClass");
                                                    $('#ModelPopUp').modal('hide');
                                                    $('#ModelPopUp').fadeOut();
                                                    count++;
                                                }
                                            }
                                        });
                                        //}
                                        //else {

                                        //}
                                    }
                                    else {
                                        //alert('hii')
                                        $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                                        $('.droppable').closest("td").find('span').removeClass("remove");
                                        $('.droppable').closest("td").find('div').removeClass("MainClass");
                                        $('#ModelPopUp').modal('hide');
                                        $('#ModelPopUp').fadeOut();
                                    }
                                },
                                error: function (e) {
                                    console.log(e);
                                }
                            });
                        }
                        else {
                            $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                            $('.droppable').closest("td").find('span').removeClass("remove");
                            //$('.droppable').closest("td").find('div').removeClass("MainClass");
                            $('#ModelPopUp').modal('hide');
                            $('#ModelPopUp').fadeOut();

                        }
                    })


                    $('.draggable').draggable().on('draggable:start', function (e, ui) {
                        $(this).addClass("text-success").find("span").css({ "color": "black" });
                        $(this).addClass("text-success").find("input").css({ "display": "block" });
                        $(this).addClass("text-success").css({ "border-left": "1px solid #2196F3", "background-color": "#a1daf6" });
                        $(this).find('input[type=hidden]').attr("type", "text");


                    })
                })
            }
        });
    </script>

    <script type="text/javascript">
        function bindDataTable1() {
            //   debugger;
            var fn = $(function () {

                $('#btnSave').on('click', function () {
                    //alert('hii')
                    var EffectiveDate = $('#<%=txtStartDate.ClientID%>').val();
                    var StartDate1 = $('#<%=txtStartDate.ClientID%>').val();
                    var EndDate1 = $('#<%=txtEndDate.ClientID%>').val();
                    var slotno = 0;
                    var DayNo = 0;
                    var dataTable = '[{';
                    var user = [];
                    var roomdynamicid = 0;var roomid = 0;
                    var txtRemark = $('#<%=txtRemark.ClientID%>').val();
                    //alert(txtRemark);
                    var count = 0;
                    $('#tblslots tr').each(function () {
                        var $tds = $(this).find('td');
                        if ($tds.length != 0) {
                            for (var i = 0; i <= 7; i++) {
                                var $currText = $tds.eq(i);

                                //debugger;
                                
                                if (i == 0) {
                                    //var $currText1 = $currText.find("span").attr('title');
                                    var $currText1 = $currText.find("span").attr('alternatetext');
                                    
                                    roomdynamicid = $currText.children('select').attr("id");

                                    roomid = $("#" + roomdynamicid, $tds).val();//$(this).find("ddlMRoom").val();
                                    
                                    if (i != 0) {
                                        DayNo = i;
                                    }
                                    if (i == 0) {
                                        slotno = $currText1;
                                        $currText1 = 0;
                                    }
                                    if ($currText1 == undefined) {
                                        $currText1 = 0;
                                    }
                                    if ($currText1 != 0) {
                                        count += 1;

                                        if (dataTable == '[{') {
                                            dataTable = dataTable + '"SlotNo":"' + slotno + '","CTNO":"' + $currText1 + '","DayNo":"' + DayNo + '","EffectiveDate":"' + EffectiveDate + '","StartDate1":"' + StartDate1 + '","EndDate1":"' + EndDate1 + '","RoomId":"' + roomid + '","Revised_Remark":"' + txtRemark + '"}'
                                            // alert(dataTable);
                                        }
                                        else {
                                            dataTable = dataTable + ',{"SlotNo":"' + slotno + '","CTNO":"' + $currText1 + '","DayNo":"' + DayNo + '","EffectiveDate":"' + EffectiveDate + '","StartDate1":"' + StartDate1 + '","EndDate1":"' + EndDate1 + '","RoomId":"' + roomid + '","Revised_Remark":"' + txtRemark + '"}'
                                        }
                                    }

                                } else {

                                    var $currText2 = $currText.find("div");

                                    $currText2.each(function (key, Userval) {
                                        //debugger;
                                        
                                        var $currText1 = $(Userval).find('span').attr('alternatetext');//$currText.find("span").attr('alternatetext');
                                        //alert($currText.html())
                                        //alert($currText2.html())
                                        //console.log(Userval)
                                        //alert($(Userval).find('span').attr('alternatetext'))
                                        //var $currText1 = $(this).find("span").attr('title');
                                        //console.log($currText);
                                        roomdynamicid = $(Userval).children('select').attr("id");
                                        //alert(roomdynamicid)
                                        roomid = $("#" + roomdynamicid, $tds).val();//$(this).find("ddlMRoom").val();
                                        
                                        if (i != 0) {
                                            DayNo = i;
                                        }
                                        if (i == 0) {
                                            slotno = $currText1;
                                            $currText1 = 0;
                                        }
                                        if ($currText1 == undefined) {
                                            $currText1 = 0;
                                        }
                                        if ($currText1 != 0) {
                                            count += 1;

                                            if (dataTable == '[{') {
                                                dataTable = dataTable + '"SlotNo":"' + slotno + '","CTNO":"' + $currText1 + '","DayNo":"' + DayNo + '","EffectiveDate":"' + EffectiveDate + '","StartDate1":"' + StartDate1 + '","EndDate1":"' + EndDate1 + '","RoomId":"' + roomid + '","Revised_Remark":"' + txtRemark + '"}'
                                                //  alert(dataTable);
                                            }
                                            else {
                                                dataTable = dataTable + ',{"SlotNo":"' + slotno + '","CTNO":"' + $currText1 + '","DayNo":"' + DayNo + '","EffectiveDate":"' + EffectiveDate + '","StartDate1":"' + StartDate1 + '","EndDate1":"' + EndDate1 + '","RoomId":"' + roomid + '","Revised_Remark":"' + txtRemark + '"}'
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    });
                    //alert(dataTable)
                    //validations
                    var ddlschool = document.getElementById("<%=ddlSchoolInstitute.ClientID%>").value;
                    var ddlSession = document.getElementById("<%=ddlSession.ClientID%>").value;
                    var ddlDegree = document.getElementById("<%=ddlDegree.ClientID%>").value;
                    var ddlBranch = document.getElementById("<%=ddlBranch.ClientID%>").value;
                    var ddlScheme = document.getElementById("<%=ddlScheme.ClientID%>").value;
                    var ddlSem = document.getElementById("<%=ddlSem.ClientID%>").value;
                    var ddlSection = document.getElementById("<%=ddlSection.ClientID%>").value;
                    var ddlSlotType = document.getElementById("<%=ddlSlotType.ClientID%>").value;
                    var txtStartDate = document.getElementById("<%=txtStartDate.ClientID%>").value;
                    var txtEndDate = document.getElementById("<%=txtEndDate.ClientID%>").value;
                    var ddlDept=document.getElementById("<%=ddlDepartment.ClientID%>").value;
                    var ddlexistingDates=document.getElementById("<%=ddlExistingDates.ClientID%>").value;
                    if (ddlschool == 0) {
                        alert('Please Select College & Scheme.');
                        document.getElementById("<%=ddlSchoolInstitute.ClientID%>").focus();
                        return false;
                    }

                    if (ddlSession == 0) {
                        alert('Please Select Session');
                        document.getElementById("<%=ddlSession.ClientID%>").focus();
                        return false;
                    }
                    <%-- if (ddlDegree == 0) {
                        alert('Please Select Degree');
                        document.getElementById("<%=ddlDegree.ClientID%>").focus();
                                    return false;
                                }
                    if (ddlBranch == 0) {
                        alert('Please select Programme/ Branch');
                        document.getElementById("<%=ddlBranch.ClientID%>").focus();
                                    return false;
                                }
                    if (ddlScheme == 0) {
                        alert('Please Select Scheme');
                        document.getElementById("<%=ddlScheme.ClientID%>").focus();
                                    return false;
                                }--%>
                    if (ddlDept == 0) {
                        alert('Please Select Department.');
                        document.getElementById("<%=ddlDegree.ClientID%>").focus();
                        return false;
                    }
                    if (ddlSem == 0) {
                        alert('Please Select Semester.');
                        document.getElementById("<%=ddlSem.ClientID%>").focus();
                        return false;
                    }
                    if (ddlSection == 0) {
                        alert('Please Select Section.');
                        document.getElementById("<%=ddlSection.ClientID%>").focus();
                        return false;
                    }
                    if (ddlSlotType == 0) {
                        alert('Please Select Slot Type.');
                        document.getElementById("<%=ddlSlotType.ClientID%>").focus();
                        return false;
                    }
                    if(ddlexistingDates==0)
                    {
                        alert('Please Select Existing Dates.');
                        document.getElementById("<%=ddlSlotType.ClientID%>").focus();
                        return false;
                    }
                    if (txtStartDate == '') {
                        alert('Please Enter Start Date');
                        document.getElementById("<%=txtStartDate.ClientID%>").focus();
                        return false;
                    }
                    if (txtEndDate == '') {
                        alert('Please Enter End Date');
                        document.getElementById("<%=txtEndDate.ClientID%>").focus();
                        return false;
                    }
                    if (count == 0) {
                        alert('Please Select Atleast One Record From List');
                        return false;
                    }

                    dataTable = dataTable + ']';

                    var AllotmentData = JSON.parse(dataTable);
                    var postdata = JSON.stringify({ 'AllotmentData': AllotmentData });

                    $.ajax({

                        url: "Revised_TimeTable.aspx/SaveTimetableAjax",
                        type: "POST",
                        //   data: "{ a: '11222' }",
                        data: postdata,
                        //dataType: "json",
                        //   data: { students: JSON.stringify(jsonObjects) },
                        contentType: "application/json; charset=utf-8",

                        success: function (response) {
                            var data = response.d;

                            data = $.parseJSON(data);
                            if (data == 1) {
                                alert('Revised Time Table Added Successfully');
                                clearTextBox();
                            }
                            else if (data == 2) {
                                alert('Please Select Dates Properly');
                                clearDateTextBox();
                            }
                            else if (data == 3) {
                                alert('Please Enter Start Date');
                                document.getElementById('<%=txtStartDate.ClientID%>').value = "";
                            }
                            else if (data == 4) {
                                alert('Please Enter End Date');
                                document.getElementById('<%=txtEndDate.ClientID%>').value = "";
                            }
                            else if (data == 5) {
                                alert('Selected Date should be greater than Session Start Date');
                                document.getElementById('<%=txtStartDate.ClientID%>').value = "";
                            }
                            else if (data == 6) {
                                alert('Selected Date should be greater than Session End Date');
                                document.getElementById('<%=txtEndDate.ClientID%>').value = "";
                            }
                            else if(data==7){
                                alert('You cannot revise the time table as teaching plan for this time table is done.');
                                clearTextBox();
                            }
                            else if(data==8){
                                alert('You cannot revise the time table as attendance for this time table is done.');
                                clearTextBox();
                            }
                            else if(data==9){
                                alert('Something went wrong.');
                                clearTextBox();
                            }
                            else {

                                alert('Something went wrong! Please refresh the browser');
                            }

                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                    return false;
                });
            });
}
    </script>
    <%--<script type="text/javascript">
        function bindDataTable1() {
            //   debugger;
            let finalDataArr = [];
            var fn = $(function () {

                const saveBtn = document.querySelector('#btnSave');

                saveBtn.addEventListener('click', () => {
                    finalDataArr = [];

                    const table = document.querySelector('#tblslots');
                    const tbody = table.querySelector('tbody');
                    const allNewContainers = tbody.querySelectorAll('div.draggable');
                    const allOldContainers = tbody.querySelectorAll('div.droppable');

                allNewContainers.forEach(container => {
                    const select = container.querySelector('select');
                        const crNo = container.querySelector('span.search_text');
                finalDataArr.push({
                    crNo: crNo.getAttribute("alternatetext"),
                    selectedVal: $(select).val()
                });
            })
            allOldContainers.forEach(container => {
                const select = container.querySelector('select');
                        const crNo = container.querySelector('span');
            finalDataArr.push({
                crNo: crNo.getAttribute("alternatetext"),
                selectedVal: $(select).val()
            });
        })


                    console.log(finalDataArr)
        })


        });
        }
    </script>--%>


    <script type="text/javascript">

        function removediv(evt) {
            if (confirm('Are you sure you want to delete this Faculty')) {
                // alert($(evt).closest('li').find('input[type=hidden]').val());

                var id = $(evt).closest('div').find('input[type=hidden]').val();
                Delete(id);
                function Delete(Id) {
                    $.ajax({
                        url: 'Revised_TimeTable.aspx/DeleteFaculty',
                        data: "{ 'Id': '" + Id + "'}",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        ///dataType: "json",
                        success: function (data) {
                            $(evt).closest('div').remove();
                            return true;
                        },
                        error: function (data) {
                            alert("Something Get Wrong!!");
                            return false;
                        }
                    });
                }
                $(evt).closest('div').remove();
                return true;

            }
            else { return false; }
        }

    </script>

    <script type="text/javascript">
        function removediv1(evt) {
            if (confirm('Are you sure you want to delete this Faculty')) {
                $(evt).closest('div').remove();
                return true;
            } else {
                return false;
            }
        }
    </script>


    <script type="text/javascript">
        function disableBtn() {
            document.getElementById("btnSave").disabled = true
        }

        function enableBtn() {
            document.getElementById("btnSave").disabled = false
        }
    </script>

    <script type="text/javascript">
        function clearTextBox() {
            document.getElementById('<%=txtRemark.ClientID%>').value = "";
            document.getElementById('<%=txtStartDate.ClientID%>').value = "";
            document.getElementById('<%=txtEndDate.ClientID%>').value = "";
            $('#rhs').hide();
            $('#ctl00_ContentPlaceHolder1_pnlSlots').hide();
            // document.getElementById("<%=ddlExistingDates.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=ddlSlotType.ClientID%>").selectedIndex = 0;
            $("#ctl00_ContentPlaceHolder1_ddlSlotType").val("0").change();
            $('#rhs').hide();
        }
    </script>

    <script type="text/javascript">
        function MyDropDownList_OnChange(dropdown) {
            var roomno = dropdown.value;
            //alert(roomno);
            var ctno = $(dropdown).parent().find("span").attr("AlternateText");

            //alert(ctno);
           
            var slot = $(dropdown).parent().closest("tr").find("[id*=lbltime]").attr("AlternateText");
            //alert(slot)

            var dayno = $(dropdown).parent().closest("td").find("div").attr("title");

            //alert(dayno);

           
            if (typeof (slot) != "undefined") {
                $.ajax({
                    url: '<%=Page.ResolveUrl("~/ACADEMIC/TimeTable/Revised_TimeTable.aspx/Get_Dynamic_Controls")%>',
                    data: '{Sessionno:' + $(<%=ddlSession.ClientID%>).val() + ', Collegeid:' + $(<%=ddlSchoolInstitute.ClientID%>).val() + ', Sectionno:' + $(<%=ddlSection.ClientID%>).val() + ', Slotno:' + slot + ', ctno:' + ctno + ', dayno:' + dayno + ', startdate:' + JSON.stringify($(<%=txtStartDate.ClientID%>).val()) + ', enddate:' + JSON.stringify($(<%=txtEndDate.ClientID%>).val()) + ', roomno:' + roomno + '}',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        var data = JSON.stringify(r.d);
                        var x = JSON.parse(data);
                        var count = 0;
                        if (x != "0") {
                            $('#ModelPopUp').modal('show');
                            $('#ModelPopUp').fadeIn();
                            document.getElementById('<%=lbluserMsg.ClientID%>').innerText = x;

                            //if (document.getElementById('<%=lbluserMsg.ClientID%>').innerText != "0") {
                            $("#btnok").on("click", function () {
                                $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                                //$('.droppable').closest("td").find('div').removeClass("MainClass");
                                //$('.droppable').closest("td").find('span').removeClass("remove");
                                $('#ModelPopUp').modal('hide');
                                $('#ModelPopUp').fadeOut();
                                count++;
                            });

                            $("#btnCancel").click(function () {
                                if (count == 0) {
                                    if (confirm('Are you sure you want to delete this Faculty') == true) {
                                        var id = $(dropdown).parent().closest(".droppable").attr("id");//$('.droppable').find('.MainClass').attr("id");
                                        //alert(id)         
                                        //var id = $('.droppable').find('.remove').closest("li").attr("id");
                                        $(dropdown).parent().closest(".droppable").remove();
                                        //$(dropdown).parent().closest('.droppable').find('span').removeClass("remove");
                                        //$('.droppable').find(".MainClass").remove();
                                        //$('.droppable').closest("td").find('span').removeClass("remove");
                                        //$('.droppable').closest("td").find('div').removeClass("MainClass");
                                        $('#ModelPopUp').modal('hide');
                                        $('#ModelPopUp').fadeOut();
                                        count++;
                                    }
                                    else {
                                        $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                                        //$('.droppable').closest("td").find('span').removeClass("remove");
                                        //$('.droppable').closest("td").find('div').removeClass("MainClass");
                                        $('#ModelPopUp').modal('hide');
                                        $('#ModelPopUp').fadeOut();
                                        count++;
                                    }
                                }
                            });
                            //}
                            //else {

                            //}
                        }
                        else {
                            //alert('hii')
                            $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                            //$('.droppable').closest("td").find('span').removeClass("remove");
                            // $('.droppable').closest("td").find('div').removeClass("MainClass");
                            $('#ModelPopUp').modal('hide');
                            $('#ModelPopUp').fadeOut();
                        }
                    },
                    error: function (e) {
                        console.log(e);
                    }
                });
            }
            else {
                $('.droppable').find(".remove").closest("li").draggable({ disabled: true });
                $('.droppable').closest("td").find('span').removeClass("remove");
                //$('.droppable').closest("td").find('div').removeClass("MainClass");
                $('#ModelPopUp').modal('hide');
                $('#ModelPopUp').fadeOut();

            }
                 


            $('.draggable').draggable().on('draggable:start', function (e, ui) {
                $(this).addClass("text-success").find("span").css({ "color": "black" });
                $(this).addClass("text-success").find("input").css({ "display": "block" });
                $(this).addClass("text-success").css({ "border-left": "1px solid #2196F3", "background-color": "#a1daf6" });
                $(this).find('input[type=hidden]').attr("type", "text");


            })
               
        }
        
        
    </script>

</asp:Content>

