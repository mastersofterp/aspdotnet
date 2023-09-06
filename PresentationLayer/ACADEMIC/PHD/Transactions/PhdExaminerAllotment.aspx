<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PhdExaminerAllotment.aspx.cs" Inherits="Academic_PhdExaminerAllotment" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <style>
           .select2-container {
            width:290px !important
        }
             .bordered {
                border:1px solid  #d2d6de; 
        }
        .group-form {
            margin:6px 0px;
        }
        @media only screen and (max-width:768px) {
            .select2-container {width:225px !important}
  
        }
       @media only screen and (max-width:1124px) {
            .select2-container {width:200px !important}
  
        }
    </style>


    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>



    <%--- // examiner 1 search box --%>
   <%-- <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtExaminer1]');
            var $ddl = $('select[id$=ddlExaminer1]');
            var $items = $('select[id$=ddlExaminer1] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {

                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>--%>



    <%--- // examiner 2 search box --%>
  <%--  <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtExaminer2]');
            var $ddl = $('select[id$=ddlExaminer2]');
            var $items = $('select[id$=ddlExaminer2] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;

                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>--%>

    <%--- // examiner 3 search box --%>
   <%-- <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtExaminer3]');
            var $ddl = $('select[id$=ddlExaminer3]');
            var $items = $('select[id$=ddlExaminer3] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>--%>

    <%--- // examiner 4 search box --%>
   <%-- <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtExaminer4]');
            var $ddl = $('select[id$=ddlExaminer4]');
            var $items = $('select[id$=ddlExaminer4] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>--%>

    <%--- // examiner 5 search box --%>
    <%--<script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtExaminer5]');
            var $ddl = $('select[id$=ddlExaminer5]');
            var $items = $('select[id$=ddlExaminer5] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>--%>

    <%--- // examiner 6 search box --%>
   <%-- <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtExaminer6]');
            var $ddl = $('select[id$=ddlExaminer6]');
            var $items = $('select[id$=ddlExaminer6] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>--%>

    <%--- // examiner 7 search box --%>
   <%-- <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtExaminer7]');
            var $ddl = $('select[id$=ddlExaminer7]');
            var $items = $('select[id$=ddlExaminer7] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>--%>

    <%--- // examiner 8 search box --%>
    <%--<script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtExaminer8]');
            var $ddl = $('select[id$=ddlExaminer8]');
            var $items = $('select[id$=ddlExaminer8] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>--%>


    <%--END : FOLLOWING CODE ALLOWS THE AUTOCOMPLETE TO BE FIRED IN UPDATEPANEL--%>

    <%-- search dropdown --%>
        <link href="../bootstrap/css/select2.min.css" rel="stylesheet" />
    <script src="../bootstrap/js/select2.min.js"></script>
 <script type="text/javascript">
     $(document).ready(function () {
         // Initialize select2
         $("#ctl00_ContentPlaceHolder1_ddlExaminer1").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer1 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer1').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlExaminer2").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer2 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer2').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlExaminer3").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer3 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer3').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });


         $("#ctl00_ContentPlaceHolder1_ddlExaminer4").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer4 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer4').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlExaminer5").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer5 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer5').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlExaminer6").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer6 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer6').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });
         //---------------------
         $("#ctl00_ContentPlaceHolder1_ddlExaminer7").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer7 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer7').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlExaminer8").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer8 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer8').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });



         $("#ctl00_ContentPlaceHolder1_ddlSupervisor").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlSupervisor option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlSupervisor').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlDRCChairman").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlDRCChairman option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlDRCChairman').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

     });

     var parameter = Sys.WebForms.PageRequestManager.getInstance();
     parameter.add_endRequest(function () {
         $("#ctl00_ContentPlaceHolder1_ddlExaminer1").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer1 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer1').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlExaminer2").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer2 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer2').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlExaminer3").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer3 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer3').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });


         $("#ctl00_ContentPlaceHolder1_ddlExaminer4").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer4 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer4').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlExaminer5").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer5 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer5').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlExaminer6").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer6 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer6').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });
         //---------------------
         $("#ctl00_ContentPlaceHolder1_ddlExaminer7").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer7 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer7').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });
         $("#ctl00_ContentPlaceHolder1_ddlExaminer8").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlExaminer8 option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlExaminer8').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });


         $("#ctl00_ContentPlaceHolder1_ddlSupervisor").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlSupervisor option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlSupervisor').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });

         $("#ctl00_ContentPlaceHolder1_ddlDRCChairman").select2();

         // Read selected option
         $('#but_read').click(function () {
             var username = $('#ctl00_ContentPlaceHolder1_ddlDRCChairman option:selected').text();
             var userid = $('#ctl00_ContentPlaceHolder1_ddlDRCChairman').val();

             $('#result').html("id : " + userid + ", name : " + username);
         });
     });
 </script>
       <%-- search dropdown --%>
    <div>
       <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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
        </asp:UpdateProgress>--%>
    </div>
    <%-- <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>--%>
    <!--Create New User-->
    
        <div class="col-sm-12">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title">Ph.D EXAMINER ALLOTMENT</h3>
                    <div class="notice"><span>Note : * marked fields are Mandatory</span></div>
                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />

                </div>

                <div class="box-body">
                    <!-- general information -->


                     <asp:UpdatePanel ID="updEdit" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row">
                    <div class="form-group col-lg-3 col-md-6 col-12">
                        <div class="label-dynamic">
                            <sup>* </sup>
                            <label>Search Criteria</label>
                        </div>

                        <%--onchange=" return ddlSearch_change();"--%>
                        <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                            <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>

                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                        <asp:Panel ID="pnltextbox" runat="server">
                            <div id="divtxt" runat="server" style="display: block">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Search String</label>
                                </div>
                                <%--onkeypress="return Validate()"--%>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlDropdown" runat="server">
                            <div id="divDropDown" runat="server" style="display: block">
                                <div class="label-dynamic">
                                    <%-- <label id="lblDropdown"></label>--%>
                                    <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                </div>
                                <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                </asp:DropDownList>

                            </div>
                        </asp:Panel>

                    </div>
                </div>
                <div class="col-12 btn-footer">
                    <%-- OnClientClick="return submitPopup(this.name);"--%>
                    <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

                    <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClose_Click1" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                </div>
                <div class="col-12 btn-footer">
                    <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                </div>

                <div class="col-12">
                    <asp:Panel ID="Panellistview" runat="server">
                        <asp:ListView ID="lvStudent" runat="server">
                            <LayoutTemplate>
                                <div>
                                    <div class="sub-heading">
                                        <h5>Student List</h5>
                                    </div>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Name
                                                    </th>
                                                    <th style="display: none">IdNo
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th><%--Branch--%>
                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th><%--Semester--%>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th>Father Name
                                                    </th>
                                                    <th>Mother Name
                                                    </th>
                                                    <th>Mobile No.
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                    </asp:Panel>
                                </div>

                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                            OnClick="lnkId_Click"></asp:LinkButton>
                                    </td>
                                    <td style="display: none">
                                        <%# Eval("idno")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                    </td>
                                    <td>
                                        <%# Eval("SEMESTERNO")%>
                                    </td>
                                    <td>
                                        <%# Eval("FATHERNAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("MOTHERNAME") %>
                                    </td>
                                    <td>
                                        <%#Eval("STUDENTMOBILE") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>
        </ContentTemplate>
             <Triggers>
                 <asp:PostBackTrigger ControlID="lvStudent" />
             </Triggers>
    </asp:UpdatePanel>



                    <asp:Panel ID="pnDisplay" runat="server" Visible="false">















                    <div class="row">
                        <div class="col-md-12">

                            <div class="box box-primary ">
                                <div class="box-header with-border">
                                    <h3 class="box-title"><b>General Info</b></h3>
                                    <div class="box-tools pull-right">
                                        <a href="#slide" data-toggle="collapse">
                                            <span class="btn btn-box-tool" data-widget="collapse">
                                                <i class="fa fa-minus"></i>
                                            </span>
                                        </a>
                                    </div>
                                </div>
                                <div id="slide" class="collaps">
                                    <div class="box-body">
                                        <div id="divGeneralInfo" style="display: block;" class="col-md-12">
                                            <asp:Panel ID="pnlId" runat="server" Visible="false">
                                                <div class="form-group col-sm-4 col-sm-offset-4">

                                                    <label><b>ID No.</b></label>
                                                    <div class="input-group date">

                                                        <asp:TextBox ID="txtIDNo" runat="server" class="form-control" TabIndex="1" Enabled="False" />
                                                        <%--  Enable the button so it can be played again --%>
                                                        <a href="#" title="Search Student for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;"></a>
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/IMAGES/search.png" TabIndex="1" data-toggle="modal" data-target="#myModal"
                                                                AlternateText="Search" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="form-group col-sm-3"  runat="server" hidden>
                                                <label>ID. No. </label>
                                                <asp:TextBox ID="txtRegNo" runat="server" class="form-control" TabIndex="2" ToolTip="Please Enter Roll No."
                                                    Enabled="false" />
                                            </div>
                                            <div class="form-group col-sm-3" runat="server" hidden>
                                                <label>Enrollment No. </label>
                                                <asp:TextBox ID="txtEnrollno" runat="server" class="form-control" TabIndex="2" ToolTip="Please Enter Enrollment No."
                                                    Enabled="false" />
                                            </div>
                                            <div class="form-group col-sm-3" runat="server" hidden>
                                                <label><span class="validstar" style="color: red">*</span> Student Name:</label>
                                                <asp:TextBox ID="txtStudentName" runat="server" MaxLength="150" TabIndex="3" ToolTip="Please Enter Student name"
                                                    Enabled="false" class="form-control" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtStudentName" />
                                                <%--<asp:RequiredFieldValidator ID="rfvStudentName" runat="server" ControlToValidate="txtStudentName"
                                                    Display="None" ErrorMessage="Please Enter Name" SetFocusOnError="True" TabIndex="8"
                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-sm-3" runat="server" hidden>
                                                <label><span class="validstar" style="color: red">*</span> Father&#39;s Name: </label>
                                                <asp:TextBox ID="txtFatherName" runat="server" TabIndex="4" ToolTip="Please Enter Father's Name"
                                                    Enabled="false" class="form-control" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtFatherName" />
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtFatherName" runat="server" ControlToValidate="txtFatherName"
                                                    Display="None" ErrorMessage="Please Enter Father Name" SetFocusOnError="True"
                                                    ValidationGroup="Academic" Visible="False"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-sm-3" runat="server" hidden>
                                                <label><span style="color: red;">*</span> Date of Joining </label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar" id="txtDateOfJoining1"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDateOfJoining" runat="server" TabIndex="14" ToolTip="Please Enter Date Of Joining"
                                                        Enabled="false" class="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="ceDateOfJoining" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtDateOfJoining1" TargetControlID="txtDateOfJoining">
                                                    </ajaxToolKit:CalendarExtender>

                                                  <%--  <asp:RequiredFieldValidator ID="rfvDateOfJoining" runat="server" ControlToValidate="txtDateOfJoining"
                                                        Display="None" ErrorMessage="Please Enter Date Of Joining" SetFocusOnError="True"
                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>

                                                </div>
                                            </div>

                                            <div class="form-group col-sm-3" runat="server" hidden>
                                                <label for="city">Department</label>
                                                <asp:DropDownList ID="ddlDepatment" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                                    Enabled="false" ToolTip="Please Select Department" class="form-control" />
                                            </div>

                                            <div class="form-group col-sm-3" runat="server" hidden>
                                                <label for="city">Status</label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" AutoPostBack="True" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Full Time</asp:ListItem>
                                                    <asp:ListItem Value="2">Part Time</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-sm-3" runat="server" hidden>
                                                <label for="city">Status Category</label>
                                                <asp:DropDownList ID="ddlStatusCat" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                                    Enabled="false" ToolTip="Please Select Status Category" class="form-control" />
                                            </div>

                                            <div class="form-group col-sm-3" runat="server" hidden>
                                                <label for="city">Admission Batch</label>
                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                                    Enabled="false" ToolTip="Please Select Admission Batch" class="form-control" />
                                            </div>

                                            <div class="form-group col-sm-3" runat="server" hidden>
                                                <label for="city"><span style="color: red;">*</span> Total No.of credits</label>
                                                <asp:TextBox ID="txtTotCredits" runat="server" TabIndex="2" ToolTip="Please Enter Total No. of Credits."
                                                    Enabled="false" class="form-control" />
                                                <%--<asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtTotCredits"
                                                    Display="None" ErrorMessage="Please Enter Credits" ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-sm-3" id="nodgc" runat="server" hidden>
                                                <label for="city">No.of&nbsp; DGC Member : </label>
                                                <asp:DropDownList ID="ddlNdgc" runat="server" TabIndex="15"
                                                    ToolTip="Please Select No.Of DGC" class="form-control" Enabled="false"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Selected="True" Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-sm-6" hidden>
                                                <label>Area of Research </label>
                                                <br />
                                                <asp:TextBox ID="txtResearch" runat="server" CssClass="unwatermarked" Enabled="false" class="form-control"> </asp:TextBox>
                                            </div>

                                            <div class="form-group col-sm-6" runat="server" hidden>
                                                <label for="city">Supervisor</label>
                                                <div class="input-group-addon">
                                                    <asp:DropDownList ID="ddlSupervisor" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                                        Enabled="false" ToolTip="Please Select Supervisor" class="form-control" AutoPostBack="True" />
                                                </div>
                                                <div class="input-group-addon">
                                                    <asp:DropDownList ID="ddlSupervisorrole" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                                        Enabled="false" TabIndex="15" ToolTip="Please Select Supervisor role" class="form-control">
                                                        <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="J">Jointly</asp:ListItem>
                                                        <asp:ListItem Value="S">Singly</asp:ListItem>
                                                        <asp:ListItem Value="T">Multiple</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                             <div class="form-group col-sm-3" id="Jointid" runat="server" hidden>
                                                <label for="city">Joint Supervisor</label>
                                                <asp:DropDownList ID="ddlCoSupervisor" runat="server" AppendDataBoundItems="True"
                                                    TabIndex="15" ToolTip="Please Select Co-Supervisor" class="form-control" Enabled="false" />
                                                <asp:Label ID="lblJointSupevisor" runat="server"></asp:Label>
                                            </div>

                                            <div class="col-sm-12" id="DivStudDetails" runat="server" visible="true">
                                                <div class="col-md-6">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item">
                                                            <b>IDNO :</b><a class="pull-right">
                                                                <asp:Label ID="lblidno" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>ROLL NO. :</b><a class="pull-right">
                                                                <asp:Label ID="lblrollno" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Student Name :</b><a class="pull-right">
                                                                <asp:Label ID="lblnames" runat="server" Font-Bold="True"></asp:Label>
                                                                <asp:HiddenField ID="hfidnos" runat="server" />
                                                            </a>
                                                        </li>

                                                        <li class="list-group-item">
                                                            <b>Date of Joining :</b><a class="pull-right">
                                                                <asp:Label ID="lbljoiningdate" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Status :</b><a class="pull-right">
                                                                <asp:Label ID="lblstatus" runat="server" Font-Bold="True"></asp:Label></a>
                                                            <asp:HiddenField ID="hfstatusno" runat="server" />
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Admission Batch :</b><a class="pull-right">
                                                                <asp:Label ID="lbladmbatch" runat="server" Font-Bold="true"></asp:Label>
                                                                <asp:HiddenField ID="hfbatchno" runat="server" />
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>No.of DGC Member :</b><a class="pull-right">
                                                                <asp:Label ID="lbldgcmember" runat="server" Font-Bold="True"></asp:Label>
                                                                <asp:HiddenField ID="hfdgcmemberno" runat="server" />
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>DRC Chairman :</b><a class="pull-right">
                                                                <asp:Label ID="lbldrcChairman" runat="server" Font-Bold="True"></asp:Label>
                                                                <asp:HiddenField ID="hfdrcChairmanno" runat="server" />
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="col-md-6">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item">
                                                            <b>Enrollment No :</b><a class="pull-right">
                                                                <asp:Label ID="lblenrollmentnos" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Father Name :</b><a class="pull-right">
                                                                <asp:Label ID="lblfathername" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Department :</b><a class="pull-right">
                                                                <asp:Label ID="lbldepartment" runat="server" Font-Bold="true"></asp:Label>
                                                                <asp:HiddenField ID="hfdepartmentno" runat="server" />
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Status Category :</b><a class="pull-right">
                                                                <asp:Label ID="lblstatuscategory" runat="server" Font-Bold="True"></asp:Label>
                                                                <asp:HiddenField ID="hfstatuscatno" runat="server" />
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Total No.of credits :</b><a class="pull-right">
                                                                <asp:Label ID="lblcredit" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Supervisor :</b><a class="pull-right">
                                                                <asp:Label ID="lblSupervisor" runat="server" Font-Bold="True"></asp:Label>
                                                                <asp:HiddenField ID="hfSupervisorno" runat="server" />
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <b>Supervisor Role :</b><a class="pull-right">
                                                                <asp:Label ID="lblSupervisorrole" runat="server" Font-Bold="True"></asp:Label>
                                                                <asp:HiddenField ID="hfJointsuperno" runat="server" />
                                                                <asp:HiddenField ID="hfSupervisorroleno" runat="server" />
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>

                                           

                                            <div class="form-group col-sm-3" id="tdremark" runat="server" hidden>
                                                <label for="city">Remark for Cancellation(Dean)</label>
                                                <asp:TextBox ID="txtRemark" runat="server" Enabled="false"> </asp:TextBox>
                                            </div>

                                            <div id="divremark" runat="server" style="color: Red; font-size: medium;" visible="false" class="form-group col-md-12">
                                                <b>DEAN Approval has been Cancelled</b>
                                            </div>

                                            <div class="form-group col-sm-4" id="trViva" runat="server" visible="false">
                                                <label for="city"><b>Date and Time of Viva-Voce Examination</b></label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtviva" runat="server" TabIndex="14" ToolTip="Please Enter Date Of Viva-Voice"
                                                        class="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtviva" TargetControlID="txtviva">
                                                    </ajaxToolKit:CalendarExtender>


                                                      <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtviva"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />

                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Date Of Viva-Voice"
                                                ControlExtender="meeDateOfBirth" ControlToValidate="txtviva" IsValidEmpty="true"
                                                InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="academic" SetFocusOnError="true" />


                                                </div>
                                            </div>

                                            <div id="divConfirm" runat="server" style="color: Green; font-size: medium;" visible="true" class="form-group col-md-4">
                                                <b>Create New Examiner </b>
                                                <asp:HyperLink ID="hlink" runat="server" Style="font-size: large; color: blue" Text=" Click Here" NavigateUrl="~/ACADEMIC/PhdExaminerMaster.aspx?pageno=1316"></asp:HyperLink>

                                            </div>
                                            <div id="div1" runat="server" style="color: Green; font-size: medium;" visible="true" class="form-group col-md-4">
                                                <b>Upload Thesis </b>
                                                <asp:HyperLink ID="HyperLink1" runat="server" Style="font-size: large; color: blue" Text="Click Here" NavigateUrl="~/ACADEMIC/PhdThesisSupervisior.aspx?pageno=2380"></asp:HyperLink>

                                            </div>

                                              


                                            <div id="divhod" runat="server" style="color: Red; font-size: medium;" visible="false" class="form-group col-md-12">
                                                <b>Above Student is not from your Department</b>
                                            </div>
                                            <div class="row">
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-sm-12">

                            <div class="box box-primary ">
                                <div class="box-header with-border" id="trDGC" runat="server">
                                    <h3 class="box-title">EXAMINER DETAILS  <span style="color: red">(The panel must include Examiner from NIT/IIT/IISC and other renowned institute clause R.16.4 )</span> </h3>
                                    <div class="box-tools pull-right">
                                        <a href="#slide1" data-toggle="collapse">
                                            <span class="btn btn-box-tool" data-widget="collapse">
                                                <i class="fa fa-minus"></i>
                                            </span>
                                        </a>
                                    </div>
                                </div>

                                <div id="slide1" class="collaps">
                                    <div class="box-body">

                                        <%-------dr info --------%>
                                        <div class="form-group  col-md-12" id="SupervisorExaminer" runat="server">
                                            <div class="row">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        
                                                        <%--Examiner 1 --%>
                                                        <div class="">
                                                            <div class="group-form col-sm-12">
                                                             
                                                                <div class="row bordered">
                                                                
                                                                    <div class="col-sm-2 group-form" style="width:160px">
                                                                        <asp:Label ID="Label1" runat="server" Text="Examiner1">
                                                                        </asp:Label>
                                                                        &nbsp;
                                                                        <asp:CheckBox ID="chkExaminer1" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-4 group-form">
                                                                        <div class="input-group-addon" style="display:none">
                                                                            <asp:TextBox ID="txtExaminer1" runat="server" class="form-control" BackColor="AntiqueWhite" PlaceHolder="Search Here"></asp:TextBox>
                                                                            <%--  <ajaxToolKit:TextBoxWatermarkExtender ID="ftv" runat="server" TargetControlID="txtExaminer1"
                                                                                WatermarkText="Search Here">
                                                                            </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                                                        </div>
                                                                        <div >
                                                                            <asp:DropDownList ID="ddlExaminer1" runat="server" AppendDataBoundItems="True"
                                                                                ToolTip="Please Select Examiner1" OnSelectedIndexChanged="ddlExaminer1_SelectedIndexChanged"
                                                                                AutoPostBack="true" CssClass="form-control">
                                                                            </asp:DropDownList>




                                                                        </div>
                                                                    </div>
                                                                  
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtMobile1" runat="server" TabIndex="2" class="form-control" ToolTip="Mobile  No." Enabled="false" />
                                                                    </div> 
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtemail1" runat="server" TabIndex="2" ToolTip="EmailId" class="form-control"
                                                                            Enabled="false" />
                                                                    </div>
                                                              

                                                                        <div class="col-sm-7 group-form" style="margin-left:-58px">
                                                                        <asp:TextBox ID="txtAllRecords" TextMode="MultiLine" Columns="50" Rows="3" Enabled="false"  runat="server"/>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--Examiner 2 --%>
                                                        <div class="">
                                                            <div class="group-form col-sm-12">
                                                                <div class="row bordered">
                                                                    <div class="col-sm-2 group-form" style="width:160px">
                                                                        <asp:Label ID="Label2" runat="server" Text="Examiner2"></asp:Label>
                                                                        &nbsp;
                                                                        <asp:CheckBox ID="chkExaminer2" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-4 group-form">
                                                                        <div class="input-group-addon" style="display:none">
                                                                            <asp:TextBox ID="txtExaminer2" runat="server" class="form-control" BackColor="AntiqueWhite" PlaceHolder="Search Here"></asp:TextBox>
                                                                            <%--<ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtExaminer2"
                                                                                WatermarkText="Search Here">
                                                                            </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                                                        </div>
                                                                        <div >
                                                                            <asp:DropDownList ID="ddlExaminer2" runat="server" class="form-control"
                                                                                OnSelectedIndexChanged="ddlExaminer2_SelectedIndexChanged"
                                                                                AppendDataBoundItems="True" AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                               
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtMobile2" runat="server" class="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtemail2" runat="server" class="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                                    </div>
                                                                     <div class="col-sm-7 group-form" style="margin-left:-58px">
                                                                        <asp:TextBox ID="txtAllRecords1" TextMode="MultiLine" Columns="50" Rows="3" Enabled="false"  runat="server"/>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--Examiner 3 --%>
                                                        <div class="">
                                                            <div class="col-sm-12 group-form">
                                                                <div class="row bordered">
                                                                    <div class="col-sm-2 group-form" style="width:160px">
                                                                        <asp:Label ID="Examiner3" runat="server" Text="Examiner3"></asp:Label>
                                                                        &nbsp;
                                                                        <asp:CheckBox ID="chkExaminer3" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-4 group-form">
                                                                        <div class="input-group-addon" style="display:none">
                                                                            <asp:TextBox ID="txtExaminer3" runat="server" class="form-control" BackColor="AntiqueWhite" PlaceHolder="Search Here"></asp:TextBox>
                                                                            <%--<ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtExaminer3"
                                                                                WatermarkText="Search Here">
                                                                            </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                                                        </div>
                                                                        <div >
                                                                            <asp:DropDownList ID="ddlExaminer3" runat="server" class="form-control"
                                                                                AppendDataBoundItems="True"
                                                                                OnSelectedIndexChanged="ddlExaminer3_SelectedIndexChanged" AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                               
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtMobile3" runat="server" class="form-control" TabIndex="2"
                                                                            ToolTip="Mobile  No." Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtemail3" runat="server" class="form-control" TabIndex="2"
                                                                            ToolTip="EmailId" Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-7 group-form" style="margin-left:-58px">
                                                                        <asp:TextBox ID="txtAllrecords3" TextMode="MultiLine" Columns="50" Rows="3" Enabled="false"  runat="server"/>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <%--Examiner 4 --%>
                                                        <div class="">
                                                            <div class="col-sm-12 group-form">
                                                                <div class="row bordered">
                                                                    <div class="col-sm-2 group-form" style="width:160px">
                                                                        <asp:Label ID="Examiner4" runat="server" Text="Examiner4">  </asp:Label>
                                                                        &nbsp;
                                                                        <asp:CheckBox ID="chkExaminer4" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-4 group-form">
                                                                        <div class="input-group-addon" style="display:none">
                                                                            <asp:TextBox ID="txtExaminer4" runat="server" class="form-control" BackColor="AntiqueWhite" PlaceHolder="Search Here"></asp:TextBox>
                                                                            <%--  <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtExaminer4"
                                                                                WatermarkText="Search Here">
                                                                            </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                                                        </div>
                                                                        <div >
                                                                            <asp:DropDownList ID="ddlExaminer4" runat="server" class="form-control"
                                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlExaminer4_SelectedIndexChanged" AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                              
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtMobile4" runat="server" class="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtemail4" runat="server" class="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-7 group-form" style="margin-left:-58px">
                                                                        <asp:TextBox ID="txtAllRecords4" TextMode="MultiLine" Columns="50" Rows="3" Enabled="false"  runat="server"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <%--Examiner 5--%>
                                                        <div class="">
                                                            <div class="col-sm-12 group-form">
                                                                <div class="row bordered">
                                                                    <div class="col-sm-2 group-form" style="width:160px">
                                                                        <asp:Label ID="Examiner5" runat="server" Text="Examiner5"> </asp:Label>
                                                                        &nbsp;
                                                                        <asp:CheckBox ID="chkExaminer5" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-4 group-form">
                                                                        <div class="input-group-addon" style="display:none">
                                                                            <asp:TextBox ID="txtExaminer5" runat="server" class="form-control" BackColor="AntiqueWhite" PlaceHolder="Search Here"></asp:TextBox>
                                                                            <%-- <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtExaminer5"
                                                                                WatermarkText="Search Here">
                                                                            </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                                                        </div>
                                                                        <div >
                                                                            <asp:DropDownList ID="ddlExaminer5" runat="server" class="form-control"
                                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlExaminer5_SelectedIndexChanged" AutoPostBack="true">
                                                                                <%--<asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                     
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtMobile5" runat="server" class="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtemail5" runat="server" class="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-7 group-form" style="margin-left:-58px">
                                                                        <asp:TextBox ID="txtAllrecords5" TextMode="MultiLine" Columns="50" Rows="3" Enabled="false"  runat="server"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <%--Examiner 6--%>
                                                        <div class="">
                                                            <div class="col-sm-12 group-form">
                                                                <div class="row bordered">
                                                                    <div class="col-sm-2 group-form" style="width:160px">
                                                                        <asp:Label ID="Examiner6" runat="server" Text="Examiner6">       </asp:Label>
                                                                        &nbsp;
                                                                        <asp:CheckBox ID="chkExaminer6" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-4 group-form">
                                                                        <div class="input-group-addon" style="display:none">
                                                                            <asp:TextBox ID="txtExaminer6" runat="server" class="form-control" BackColor="AntiqueWhite" PlaceHolder="Search Here"></asp:TextBox>
                                                                            <%-- <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtExaminer6"
                                                                                WatermarkText="Search Here">
                                                                            </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                                                        </div>
                                                                        <div >
                                                                            <asp:DropDownList ID="ddlExaminer6" runat="server" class="form-control"
                                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlExaminer6_SelectedIndexChanged" AutoPostBack="true">
                                                                                <%--<asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                              
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtMobile6" runat="server" class="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtemail6" runat="server" class="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-7 group-form" style="margin-left:-58px">
                                                                        <asp:TextBox ID="txtAllRecords6" TextMode="MultiLine" Columns="50" Rows="3" Enabled="false"  runat="server"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <%--Examiner 7--%>
                                                        <div class="">
                                                            <div class="col-sm-12 group-form">
                                                                <div class="row bordered">
                                                                    <div class="col-sm-2 group-form" style="width:160px">
                                                                        <asp:Label ID="Examiner7" runat="server" Text="Examiner7"> </asp:Label>
                                                                        &nbsp; 
                                                                        <asp:CheckBox ID="chkExaminer7" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-4 group-form">
                                                                        <div class="input-group-addon" style="display:none">
                                                                            <asp:TextBox ID="txtExaminer7" runat="server" class="form-control" BackColor="AntiqueWhite" PlaceHolder="Search Here"></asp:TextBox>
                                                                            <%--<ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtExaminer7"
                                                                                WatermarkText="Search Here">
                                                                            </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                                                        </div>
                                                                        <div>
                                                                            <asp:DropDownList ID="ddlExaminer7" runat="server" class="form-control"
                                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlExaminer7_SelectedIndexChanged" AutoPostBack="true">
                                                                                <%--<asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                         
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtMobile7" runat="server" class="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtemail7" runat="server" class="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-7 group-form" style="margin-left:-58px">
                                                                        <asp:TextBox ID="txtAllrecords7" TextMode="MultiLine" Columns="50" Rows="3" Enabled="false"  runat="server"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <%--Examiner 8--%>
                                                        <div class="">
                                                            <div class="col-sm-12 group-form">
                                                                <div class="row bordered">
                                                                    <div class="col-sm-2 group-form" style="width:160px">
                                                                        <asp:Label ID="Examiner8" runat="server" Text="Examiner8">   </asp:Label>
                                                                        &nbsp;
                                                                        <asp:CheckBox ID="chkExaminer8" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-4 group-form">
                                                                        <div class="input-group-addon" style="display:none">
                                                                            <asp:TextBox ID="txtExaminer8" runat="server" class="form-control" BackColor="AntiqueWhite" PlaceHolder="Search Here"></asp:TextBox>
                                                                            <%-- <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtExaminer8"
                                                                                WatermarkText="Search Here">
                                                                            </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                                                        </div>
                                                                        <div >
                                                                            <asp:DropDownList ID="ddlExaminer8" runat="server" class="form-control"
                                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlExaminer8_SelectedIndexChanged" AutoPostBack="true">
                                                                                <%--<asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtMobile8" runat="server" class="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                                    </div>
                                                                    <div class="col-sm-3 group-form" style="display:none">
                                                                        <asp:TextBox ID="txtemail8" runat="server" class="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                                    </div>
                                                                       <div class="col-sm-7 group-form" style="margin-left:-58px">
                                                                        <asp:TextBox ID="txtAllRecords8" TextMode="MultiLine" Columns="50" Rows="3" Enabled="false"  runat="server"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                      
                                                        <%--Examiner 9--%>
                                                        <div class="col-sm-12" style="display: none">
                                                            <div class="col-sm-4">
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <asp:Label ID="Examiner9" runat="server" Text="Examiner9">  </asp:Label>
                                                                        &nbsp; 
                                                                        <asp:CheckBox ID="chkExaminer9" runat="server" />
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlExaminer9" runat="server" class="form-control"
                                                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddlExaminer9_SelectedIndexChanged" AutoPostBack="true">
                                                                        <%--<asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">Mobile No:</div>
                                                                    <asp:TextBox ID="txtMobile9" runat="server" class="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        Email ID:
                                                                    </div>
                                                                    <asp:TextBox ID="txtemail9" runat="server" class="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <%--Examiner 10--%>
                                                        <div class="col-sm-12" style="display: none">
                                                            <div class="col-sm-4">
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <asp:Label ID="Examiner10" runat="server" Text="Examiner10">     </asp:Label>
                                                                        <asp:CheckBox ID="chkExaminer10" runat="server" />
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlExaminer10" runat="server" class="form-control"
                                                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddlExaminer10_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                        <%--<asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">Mobile No:</div>
                                                                    <asp:TextBox ID="txtMobile10" runat="server" class="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        Email ID:
                                                                    </div>
                                                                    <asp:TextBox ID="txtemail10" runat="server" class="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="box box-primary ">
                                                    <div class="box-header with-border" id="Div4" runat="server">
                                                        <h3 class="box-title">Pre-Synopsis/Synopsis Details  </h3>
                                                        <div class="box-tools pull-right">
                                                            <a href="#slide2" data-toggle="collapse">
                                                                <span class="btn btn-box-tool" data-widget="collapse">
                                                                    <i class="fa fa-minus"></i>
                                                                </span>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group col-sm-12">
                                                    <div class="form-group col-sm-6">
                                                        <div class="form-group col-sm-12" id="divsynopsis" runat="server" visible="false">
                                                            <label><span style="color: red;">*</span>Upload Synopsis File :</label>
                                                            <asp:FileUpload ID="fuSynopsis" runat="server" CssClass="form-control" ToolTip="Select file to upload" accept=".pdf" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="file Required"
                                                        ControlToValidate="fuSynopsis" ValidationGroup="submit"
                                                        runat="server" Display="Dynamic" ForeColor="Red" />--%>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.PDF|.pdf)$"
                                                                ControlToValidate="fuSynopsis" runat="server" ForeColor="Red" ErrorMessage="Please select a valid PDF File ."
                                                                ValidationGroup="submit" Display="Dynamic" />
                                                            <asp:ListView ID="lvUpload" runat="server" Visible="true">
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid" class="vista-grid">
                                                                        <div class="titlebar">
                                                                            <%-- <h4>
                                                                    <label class="label label-default">Synopsis List</label></h4>--%>
                                                                        </div>
                                                                        <table id="example1" class="table table-bordered table-hover text-center">
                                                                            <tr class="bg-light-blue">
                                                                                <th>Uploaded Synopsis File
                                                                                </th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkDownloadDoc" runat="server" OnClick="lnkDownloadDoc_Click"
                                                                                class="form-control" Text='<%# Eval("FILENAME") %>' CommandArgument='<%# Eval("IDNO") %>'>
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hdfFilename" runat="server" Value='<%#Eval("PATH") %> ' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-sm-6" id="divpresynopsis" runat="server" visible="false">
                                                        <div class="form-group col-sm-12">
                                                            <label><span style="color: red;">*</span>Upload Pre-Synopsis Presentation Office order:</label>
                                                            <asp:FileUpload ID="fupresynopsis" runat="server" CssClass="form-control" ToolTip="Select file to upload"
                                                                accept=".pdf" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="file Required"
                                                                ControlToValidate="fupresynopsis" ValidationGroup="submit"
                                                                runat="server" Display="Dynamic" ForeColor="Red" />--%>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.PDF|.pdf)$"
                                                                ControlToValidate="fupresynopsis" runat="server" ForeColor="Red"
                                                                ErrorMessage="Please select a valid PDF File ."
                                                                ValidationGroup="submit" Display="Dynamic" />
                                                            <asp:ListView ID="lvpresynopsis" runat="server" Visible="true">
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid" class="vista-grid">
                                                                        <div class="titlebar">
                                                                            <%-- <h4><label class="label label-default">Synopsis List</label></h4>--%>
                                                                        </div>
                                                                        <table id="example1" class="table table-bordered table-hover text-center">
                                                                            <tr class="bg-light-blue">
                                                                                <th>Uploaded Pre-Synopsis Presentation File
                                                                                </th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkDownloadDoc" runat="server" OnClick="lnkDownloadDoc_Click"
                                                                                class="form-control" Text='<%# Eval("FILENAME") %>' CommandArgument='<%# Eval("IDNO") %>'>
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hdfFilename" runat="server" Value='<%#Eval("PATH") %> ' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>

                                                        <div class="form-group col-sm-12">

                                                            <label for="city"><span style="color: red;">*</span> Pre-Synopsis Presentation Date</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"  id="txtdatepre1"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtdatepre" runat="server" TabIndex="14" ToolTip="Please Enter Date Of Viva-Voice"
                                                                    class="form-control" />
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="txtdatepre1" TargetControlID="txtdatepre">
                                                                </ajaxToolKit:CalendarExtender>


                                                                 <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtdatepre"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" EmptyValueMessage="Please Enter Pre-Synopsis Presentation Date"
                                                ControlExtender="meeDateOfBirth" ControlToValidate="txtdatepre" IsValidEmpty="true"
                                                InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="academic" SetFocusOnError="true" />

                                                                <%-- <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                                    MaskType="Date" TargetControlID="txtdatepre" />

                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeDateOfJoining"
                                                                    ControlToValidate="txtdatepre" Display="None" EmptyValueBlurredText="*"
                                                                    EmptyValueMessage="Please Enter Date Of Joining" ErrorMessage="Please Select Date"
                                                                    InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid" IsValidEmpty="False"
                                                                    SetFocusOnError="True" TooltipMessage="Input a date" />--%>
                                                            </div>
                                                        </div>

                                                    </div>

                                                </div>
                                                <div id="Div3" class="collapse in">
                                                    <div class="box-body">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>




                    <div class="row">
                        <div class="col-sm-12" runat="server" hidden>
                            <div class="box box-primary ">
                                <div class="box-header with-border" id="trdrc" runat="server">
                                    <h3 class="box-title">Recommendation of the Departmental Research Committee(DRC) </h3>
                                    <div class="box-tools pull-right">
                                        <a href="#slide2" data-toggle="collapse">
                                            <span class="btn btn-box-tool" data-widget="collapse">
                                                <i class="fa fa-minus"></i>
                                            </span>
                                        </a>
                                    </div>
                                </div>
                                <div id="slide2" class="collapse in">
                                    <div class="box-body">
                                        <div id="divdrc" runat="server" class="col-md-12" hidden>
                                            The DRC recommends the registration of Mr./Mrs.<asp:Label ID="lblname" runat="server"
                                                Text="name" Font-Bold="true"></asp:Label>&nbsp;<asp:Label ID="partfull" runat="server"></asp:Label>student
                                                       with effect from
                                                <asp:Label ID="lbldate" runat="server" Font-Bold="true"></asp:Label>
                                            and also recommends the appointment of supervisor (s) as he / she / they satisfy
                                                rule R.7 of PhD ordinance (supervisors' Bio-data with list of publications and experience
                                                be enclosed) and formation of DGC as indicated above.

                                        </div>

                                        <div id="trdrc1" runat="server" class="col-sm-12" visible="false">
                                            <div class="form-group col-sm-8">
                                                <label>A DRC Chairman  </label>

                                                <span class="input-group-addon">
                                                    <asp:DropDownList ID='ddlDRCChairman' runat="server" TabIndex="15" ToolTip="Please Select A DRC nominee"
                                                        Enabled="false" class="form-control" />
                                                </span>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                

                <div class="box-footer text-center ">
                    &nbsp;<asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                    <p>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="131" Text="Submit" ValidationGroup="Academic" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" OnClick="btnCancel_Click" TabIndex="132" Text="Cancel" />
                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="133" Text="Examiner Report" Visible="false" />
                        <asp:Button ID="btnReject" runat="server" CssClass="btn btn-danger" OnClick="btnReject_Click" TabIndex="134" Text="Reject" Visible="false" />
                        <asp:Button ID="btnApply" runat="server" CssClass="btn btn-info" OnClick="btnApply_Click" TabIndex="134" Text="Apply Student List" />
                        <asp:Button ID="btnAppoint" runat="server" CssClass="btn btn-primary" OnClick="btnAppoint_Click" TabIndex="134" Text="Examiner Appointment Letter" Visible="false" />
                        <asp:Button ID="btnExternalReport" runat="server" CssClass="btn btn-info" OnClick="btnExternalReport_Click" TabIndex="134" Text="ODC Examiner Report" Visible="false" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Qual" />
                        <p>
                            &nbsp;<asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EntranceExam" />

                </div>
                        </asp:Panel>
            </div>
        

    

    </div>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
   



    <div id="divMsg" runat="server">
    </div>

     <script type="text/javascript">

         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $('#id1').dataTable({
                 paging: false,
                 searching: true,
                 bDestroy: true
             });
         });
        </script>
     <script type="text/javascript" lang="javascript">

         $(document).ready(function () {
             debugger
             $("#<%= pnltextbox.ClientID %>").hide();

             $("#<%= pnlDropdown.ClientID %>").hide();
         });
         function submitPopup(btnsearch) {

             debugger
             var rbText;
             var searchtxt;

             var e = document.getElementById("<%=ddlSearch.ClientID%>");
             var rbText = e.options[e.selectedIndex].text;
             var ddlname = e.options[e.selectedIndex].text;
             if (rbText == "Please Select") {
                 alert('Please Select Search Criteria.')
                 $(e).focus();
                 return false;
             }

             else {


                 if (rbText == "ddl") {
                     var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>
</asp:Content>
