<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BusBooking.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_BusBooking" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity" DisplayAfter="0">
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
    <style type="text/css">
        .modalPopup {
            box-shadow: rgba(0, 0, 0, 0.4) 0px 2px 4px, rgba(0, 0, 0, 0.3) 0px 7px 13px -3px, rgba(0, 0, 0, 0.2) 0px -3px 0px inset;
        }

        .modal-xl {
            max-width: 1140px;
        }

        #holder {
            height: 200px;
            width: 550px;
            background-color: #F5F5F5;
            border: 1px solid #A4A4A4;
            margin-left: 10px;
        }

        .btn-img {
            height: 25px;
            width: 25px;
        }

        #place {
            position: relative;
            margin: 7px;
        }

            #place a {
                font-size: 0.6em;
            }

            #place li {
                list-style: none outside none;
                position: absolute;
            }

                #place li:hover {
                    background-color: yellow;
                }

            #place .seat {
                background: url("images/available_seat_img.gif") no-repeat scroll 0 0 transparent;
                height: 20px;
                width: 20px;
                display: block;
            }

            #place .selectedSeat {
                background-image: url("images/booked_seat_img.gif");
            }

            #place .selectingSeat {
                background-image: url("images/selected_seat_img.gif");
            }

            #place .row-3, #place .row-4 {
                margin-top: 10px;
            }

        #seatDescription li {
            verticle-align: middle;
            list-style: none outside none;
            padding-left: 35px;
            height: 35px;
            float: left;
        }
    </style>
    <style>
        .new {
            width: 100px;
            text-align: center;
            background-color: transparent;
        }

        td {
            /*padding: 5px;*/
            text-align: center;
        }

        .main {
            background-color: #fff;
            border-top: 1px solid #fff !important;
        }

        .table-bordered, .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #5b5656;
        }

        .note-div span {
            font-size: 15px;
            font-weight: 600;
        }
    </style>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BUS BOOKING</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Name</label>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtname" ReadOnly="True" meta:resourcekey="txtnameResource1"></asp:TextBox>
                                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1"></asp:Label>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" TabIndex="3" AppendDataBoundItems="True" Enabled="False"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Session" meta:resourcekey="ddlSessionResource1">
                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">Please Select </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Semester </label>
                                                </div>
                                                <asp:DropDownList ID="ddlbSemester" runat="server" TabIndex="3" AppendDataBoundItems="True"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Stop Name" Enabled="False" meta:resourcekey="ddlbSemesterResource1">
                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource2">Please Select </asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divbusstr" runat="server" visible="False">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Bus Structure </label>
                                                </div>
                                                <asp:DropDownList ID="ddlStructure" runat="server" TabIndex="3" AppendDataBoundItems="True"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Bus Structure Name" meta:resourcekey="ddlStructureResource1">
                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource3">Please Select </asp:ListItem>
                                                </asp:DropDownList>

                                            </div>


                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Route </label>
                                                </div>
                                                <asp:DropDownList ID="ddlRoute" runat="server" TabIndex="3" AppendDataBoundItems="True"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Stop Name" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlRouteResource1">
                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource4">Please Select </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Stop</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStop" runat="server" TabIndex="3" AppendDataBoundItems="True"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Stop Name" OnSelectedIndexChanged="ddlStop_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlStopResource1">
                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource5">Please Select </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                         

                                            <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="divSeats" visible="False">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Bus Seats </label>
                                                </div>
                                                <asp:TextBox ID="txtBusSeate" runat="server" ReadOnly="True" meta:resourcekey="txtBusSeateResource1"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="div1" visible="False">
                                                <div class="row">
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Fees</label>
                                                        </div>
                                                        <asp:TextBox ID="lblfees" runat="server" ReadOnly="True" meta:resourcekey="lblfeesResource1"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Paid Fees</label>
                                                        </div>
                                                        <asp:TextBox ID="lblPfees" runat="server" ReadOnly="True" meta:resourcekey="lblfeesResource1"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Total Fees</label>
                                                        </div>
                                                        <asp:TextBox ID="lblTfees" runat="server" ReadOnly="True" meta:resourcekey="lblfeesResource1"></asp:TextBox>
                                                    </div>
                                                </div>


                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="div5" visible="False">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Bus Route Starting Time</label>
                                                </div>
                                                <asp:TextBox ID="txtStopStarttime" runat="server" ReadOnly="True" meta:resourcekey="txtStopStarttimeResource1"></asp:TextBox>

                                            </div>

                                               <%--//------09-10-2023------start------%>

                                             <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="devreceiptcode" visible="False">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Receipt Id</label>
                                                </div>
                                                <asp:DropDownList ID="ddlReceipt" runat="server" TabIndex="3" AppendDataBoundItems="True"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Receipt Id" >
                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource5">Please Select </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <%--//------09-10-2023------end------%>


                                        </div>
            </div>


                                    <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="divimg" visible="False">
                                        <div class="row">
                                            <div class="col-12 ">
                                                <div class="sub-heading">
                                                    <h5>
                                                        <asp:Label runat="server" ID="lblrout" Font-Size="Large" Style="text-align: center" meta:resourcekey="lblroutResource1"></asp:Label></h5>
                                                </div>
                                                <asp:Image ID="lblImage" runat="server" Height="200px" Width="500px" meta:resourcekey="lblImageResource1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowStrbtnShowStr" runat="server" CssClass="btn btn-info" Text="Show Structure" OnClientClick="getstructure();" OnClick="btnShowStr_Click" meta:resourcekey="btnShowStrbtnShowStrResource1"></asp:Button>
                                <asp:Button ID="btnShowBusSeats" runat="server" TabIndex="10"
                                    Text="Show" ValidationGroup="Submit" CssClass="btn btn-info" ToolTip="Click here to Show Bus Seats" OnClick="btnShowBusSeats_Click" Visible="False" meta:resourcekey="btnShowBusSeatsResource1" />
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="12" Text="Book And Pay Now" OnClick="btnSubmit_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" meta:resourcekey="btnSubmitResource1" />
                                 <asp:Button ID="btnReport" runat="server" TabIndex="12" Text="Receipt" OnClick="btnReport_Click"
                                    CssClass="btn btn-info" ToolTip="Click here to Generate Receipt" meta:resourcekey="btnReport" Enabled="false"/>
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                                    TabIndex="11" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset" meta:resourcekey="btnCancelResource1" />
                            </div>



                            <div class="row">
                                <div class="form-group col-lg-6 col-md-4 col-12">
                                    <div class="form-group col-lg-12 col-md-12 col-12" runat="server" id="structurediscription" visible="False">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i style="color: #EB9999;" class="fa fa-star" aria-hidden="true"></i><span>Red Highlited Seat Can Booked By Girls Student </span></p>
                                            <p><i style="color: #3685e5;" class="fa fa-star" aria-hidden="true"></i><span>Blue Highlited Seat Can Booked By Boys Student</span></p>
                                            <p><i style="color: #ffff00;" class="fa fa-star" aria-hidden="true"></i><span>Yellow Highlited Seat Can Booked By Staffs</span></p>
                                            <p><i style="color: #8b8585;" class="fa fa-star" aria-hidden="true"></i><span>Gray Highlited Seat Can Booked By Block</span></p>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12 col-md-12 col-12" runat="server" id="seatestatus" visible="False" style="background-color: lightgray">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:Image ID="image1" runat="server" CssClass="btn-img pr-1" ImageUrl="~/Images/available_seat_img_New.png" meta:resourcekey="image1Resource1" /><span style="font-size: 15px; font-weight: 600">Available Seat</span>
                                            </div>

                                            <div class="col-md-4">
                                                <asp:Image ID="image3" CssClass="btn-img pr-1" runat="server" ImageUrl="~/Images/selected_seat_img.png" meta:resourcekey="image3Resource1" />
                                                <span style="font-size: 15px; font-weight: 600">Selected Seat</span>
                                            </div>

                                            <div class="col-md-4">
                                                <asp:Image ID="image2" CssClass="btn-img pr-1" runat="server" ImageUrl="~/Images/booked_seat_img.png" meta:resourcekey="image2Resource1" />
                                                <span style="font-size: 15px; font-weight: 600">Booked Seat</span>
                                            </div>
                                        </div>

                                    </div>


                                    <div class="form-group col-lg-12 col-md-12 col-12" runat="server" id="divroutepath" visible="False">
                                        <div class="row">
                                            <div class=" note-div">
                                                <h5 class="heading">Route Path</h5>

                                                <asp:Label ID="lblRoutPath" runat="server" Style="background-color: lightgray" meta:resourcekey="lblRoutPathResource1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-6 col-md-8 col-12">
                                    <asp:UpdatePanel ID="updDocument" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                                                <asp:Label ID="lblStartTime" runat="server" meta:resourcekey="lblStartTimeResource1"></asp:Label>
                                                <asp:Label ID="lblElapsedTime" runat="server" meta:resourcekey="lblElapsedTimeResource1"></asp:Label>
                                                <asp:ListView ID="lvStructure" runat="server" OnItemDataBound="lvStructure_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <div class="sub-heading">
                                                                <h5>Bus Structure</h5>

                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 300px" id="">
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td runat="server" id="tdcol1" style="text-align: center; vertical-align: central">
                                                                <asp:ImageButton ID="byncol1" runat="server" CssClass="btn-img" AlternateText="Edit Record" CausesValidation="False" CommandArgument='<%# Eval("COL1") %>' ImageUrl="~/Images/available_seat_img_New.png" ToolTip='<%# Eval("COL1") %>' OnClick="byncol1_Click" meta:resourcekey="byncol1Resource1" />
                                                                <b>
                                                                    <asp:Label ID="lb" Text='<%# Eval("COL1") %>' runat="server" Style="font-size: larger; text-align: center" meta:resourcekey="lbResource1"></asp:Label>
                                                                </b>
                                                            </td>
                                                            <td runat="server" id="tdcol2">
                                                                <asp:ImageButton ID="byncol2" runat="server" CssClass="btn-img" AlternateText="Edit Record" CausesValidation="False" CommandArgument='<%# Eval("COL2") %>' ImageUrl="~/Images/available_seat_img_New.png" ToolTip='<%# Eval("COL2") %>' OnClick="byncol2_Click" meta:resourcekey="byncol2Resource1" />
                                                                <b>
                                                                    <asp:Label ID="Label4" Text='<%# Eval("COL2") %>' runat="server" Style="font-size: larger; text-align: center" meta:resourcekey="Label4Resource1"></asp:Label>
                                                                </b>
                                                            </td>
                                                            <td runat="server" id="tdcol3">
                                                                <asp:ImageButton ID="byncol3" runat="server" CssClass="btn-img" AlternateText="Edit Record" CausesValidation="False" CommandArgument='<%# Eval("COL3") %>' ImageUrl="~/Images/available_seat_img_New.png" ToolTip='<%# Eval("COL3") %>' OnClick="byncol3_Click" meta:resourcekey="byncol3Resource1" />
                                                                <b>
                                                                    <asp:Label ID="Label5" Text='<%# Eval("COL3") %>' runat="server" Style="font-size: larger; text-align: center" meta:resourcekey="Label5Resource1"></asp:Label>
                                                                </b>
                                                            </td>
                                                            <td runat="server" id="tdcol4">
                                                                <asp:ImageButton ID="byncol4" runat="server" CssClass="btn-img" AlternateText="Edit Record" CausesValidation="False" CommandArgument='<%# Eval("COL4") %>' ImageUrl="~/Images/available_seat_img_New.png" ToolTip='<%# Eval("COL4") %>' OnClick="byncol4_Click" meta:resourcekey="byncol4Resource1" />
                                                                <b>
                                                                    <asp:Label ID="Label6" Text='<%# Eval("COL4") %>' runat="server" Style="font-size: larger; text-align: center" meta:resourcekey="Label6Resource1"></asp:Label>
                                                                </b>
                                                            </td>
                                                            <td runat="server" id="tdcol5">
                                                                <asp:ImageButton ID="byncol5" runat="server" CssClass="btn-img" AlternateText="Edit Record" CausesValidation="False" CommandArgument='<%# Eval("COL5") %>' ImageUrl="~/Images/available_seat_img_New.png" ToolTip='<%# Eval("COL5") %>' OnClick="byncol5_Click" meta:resourcekey="byncol5Resource1" />
                                                                <b>
                                                                    <asp:Label ID="Label7" Text='<%# Eval("COL5") %>' runat="server" Style="font-size: larger; text-align: center" meta:resourcekey="Label7Resource1"></asp:Label>
                                                                </b>
                                                            </td>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div3" runat="server" visible="False">
                                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1"></asp:Label>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1"></asp:Label>
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>

                                        </ContentTemplate>


                                    </asp:UpdatePanel>

                                </div>
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlList" runat="server" meta:resourcekey="pnlListResource1">
                                    <asp:ListView ID="lvBusStructure" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Route Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT </th>
                                                            <th>ROUTE NUMBER</th>
                                                            <th>ROUTE NAME </th>
                                                            <th>ROUTE PATH </th>
                                                            <th>DISTANCE (IN KM)</th>
                                                            <th>VEHICLE TYPE</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="False" CommandArgument='<%# Eval("ROUTEID") %>' ImageUrl="~/Images/edit.png" ToolTip="Edit Record" meta:resourcekey="btnEditResource1" />
                                                </td>
                                                <td><%# Eval("ROUTE_NUMBER")%></td>
                                                <td><%# Eval("ROUTENAME")%></td>
                                                <td><%# Eval("ROUTEPATH")%></td>
                                                <td><%# Eval("DISTANCE")%></td>
                                                <td><%#Eval("VEHICLE_TYPE")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>


                                </asp:Panel>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="False">
                                <asp:Label ID="lblBlobConnectiontring" runat="server" meta:resourcekey="lblBlobConnectiontringResource1"></asp:Label>
                                <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                <asp:Label ID="lblBlobContainer" runat="server" meta:resourcekey="lblBlobContainerResource1"></asp:Label>
                                <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div id="page-wrapper">
                <header class="main-header">

                    <nav class="navbar navbar-expand-lg navbar-dark fixed-top">
                        <!-- Brand -->
                        <a class="navbar-brand ml-1" href="#">
                            <img id="Img1" alt="logo" runat="server" src="Images/nophoto.jpg" />
                        &nbsp;</a></nav>
                </header>
                <section>
                    <div class="box-body">
                        <div id="div4" runat="server"></div>
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup modal-xl" Style="display: none;" meta:resourcekey="Panel2Resource1">
                            <div class="container">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="sub-heading">
                                            <h5>Terms and conditions</h5>
                                        </div>

                                        <div class="DocumentList">
                                            <div class=" col-lg-12 col-md-6 col-12">
                                                <asp:Label ID="lblterms" runat="server" meta:resourcekey="lbltermsResource1"></asp:Label>

                                                <asp:Repeater ID="rptData" runat="server">
                                                    <ItemTemplate>
                                                        <div style="list-style-type: none; line-height: normal">
                                                            <p class="mb-0"><%# Eval("TERMS_AND_CONDITIONS") %></p>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <p>
                                                    <asp:CheckBox ID="checIsConform" runat="server" meta:resourcekey="checIsConformResource1" />
                                                    <span style="color: #FF0000; font-weight: bold">You agree to the set out by this site, including our Cookie Use.</span>
                                                </p>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="btn-footer col-12">
                                    <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="btn btn-primary" OnClick="btnokyes_Click" meta:resourcekey="btnOkResource1" />
                                    <asp:Button ID="btnClose" runat="server" Text="CANCEL" CssClass="btn btn-outline-danger" meta:resourcekey="btnCloseResource1" />
                                </div>

                            </div>
                            <ajaxToolKit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                                BackgroundCssClass="modalBackground" RepositionMode="RepositionOnWindowScroll"
                                TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="Panel2" DynamicServicePath="" Enabled="True">
                            </ajaxToolKit:ModalPopupExtender>
                            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" meta:resourcekey="hiddenTargetControlForModalPopupResource1" />
                        </asp:Panel>
                    </div>
            </section>
                 <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



    <script type="text/javascript">










        //--------
        //function getstructure() {
        debugger;
        var settings = {
            rows: 5,
            cols: 10,
            rowCssPrefix: 'row-',
            colCssPrefix: 'col-',
            seatWidth: 40,
            seatHeight: 35,
            seatCss: 'seat',
            selectedSeatCss: 'selectedSeat',
            selectingSeatCss: 'selectingSeat'


        };

        var init = function (reservedSeat) {
            var str = [], seatNo, className;
            for (i = 0; i < settings.rows; i++) {
                for (j = 0; j < settings.cols; j++) {
                    seatNo = (i + j * settings.rows + 1);
                    className = settings.seatCss + ' ' + settings.rowCssPrefix + i.toString() + ' ' + settings.colCssPrefix + j.toString();
                    if ($.isArray(reservedSeat) && $.inArray(seatNo, reservedSeat) != -1) {
                        className += ' ' + settings.selectedSeatCss;
                    }
                    str.push('<li class="' + className + '"' +
                              //'style="top:' + (i * settings.seatHeight).toString() + 'px;left:' + (j * settings.seatWidth).toString() + 'px">' +
                              'style="top:' + (i * settings.seatHeight).toString() + 'px;left:' + (j * settings.seatWidth).toString() + 'px;font-size:20px;font-weight-bold">' +
                              '<a title="' + seatNo + '">' + seatNo + '</a>' +
                              '</li>');
                }
            }
            $('#place').html(str.join(''));
        };
        //case I: Show from starting
        //init();

        //Case II: If already booked
        var bookedSeats = [5, 10, 25];
        init(bookedSeats);

        $('.' + settings.seatCss).click(function () {
            if ($(this).hasClass(settings.selectedSeatCss)) {
                alert('This seat is already reserved');
            }
            else {
                $(this).toggleClass(settings.selectingSeatCss);
            }
        });

        $('#btnShow').click(function () {
            var str = [];
            $.each($('#place li.' + settings.selectedSeatCss + ' a, #place li.' + settings.selectingSeatCss + ' a'), function (index, value) {
                str.push($(this).attr('title'));
            });
            alert(str.join(','));
        })

        $('#btnShowNew').click(function () {
            var str = [], item;
            $.each($('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) {
                item = $(this).attr('title');
                str.push(item);
            });
            alert(str.join(','));
        })
        //}

    </script>
</asp:Content>

