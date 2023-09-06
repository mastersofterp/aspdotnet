<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ITLEResult.aspx.cs" Inherits="Itle_ITLEResult" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <%--<title>MasterSoft ERP Sloution RPVT LTD</title>--%>
    <link rel="shortcut icon" href="Images/nophoto.jpg" type="image/x-icon" id="lnklogo">

    <%-- Added Google Sign In on Date 21/09/2020 by DEEPALI--%>
    <%--As disccused with Umesh sir commnted this google sign in button section on date 30-12-2021. will work on this in second phase--%>
    <%--<meta name="google-signin-client_id" content="756005764126-6b0tt497vp345vfn0nso5reuonq5o11l.apps.googleusercontent.com">--%>
    <%-- End Google Sign In on Date 21/09/2020 by DEEPALI--%>



    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css")%>" rel="stylesheet" />
    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/newcustom.css")%>" rel="stylesheet" />

    <%-- scripts added by gaurav--%>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>




    <%--<script defer src="<%#Page.ResolveClientUrl("~/bootstrap/datatable/jquery3.3.1.js")%>"></script>--%>



    <style type="text/css">
        .style2 {
            width: 185px;
        }

        .bg-blue-gradient {
            background: #0073b7!important;
            background: -webkit-gradient(linear,left bottom,left top,color-stop(0,#0073b7),color-stop(1,#0089db))!important;
        }

        .panel-primary > .panel-heading {
            color: #fff;
            background-color: #337ab7;
            border-color: #337ab7;
        }

        .panel-body {
            padding: 15px;
        }

        .bg-maroon-gradient {
            background: #d81b60!important;
            background: -webkit-gradient(linear,left bottom,left top,color-stop(0,#d81b60),color-stop(1,#e73f7c))!important;
            background: -ms-linear-gradient(bottom,#d81b60,#e73f7c)!important;
            background: -moz-linear-gradient(center bottom,#d81b60 0,#e73f7c 100%)!important;
            background: -o-linear-gradient(#e73f7c,#d81b60)!important;
            color: #fff;
        }
         .myRadioButtonList input {
            /*font: inherit;
            font-size: 0.875em; /* 14px / 16px */
            /*color: #494949;
            margin-bottom: 12px;
            margin-top: 5px;
            margin-right: 10px !important;*/
            height: 15px; 
            width: 15px;
             /*background-color: green;*/
            border-radius: 50%; 
            display: inline-block;


        }
    </style>


    <style>
        .panel-heading h2 {
            margin: 0;
        }

        #divCollegename {
            margin-bottom: 0;
        }

        @media only screen and (max-width:767px) {
            #divCollegename {
                font-size: 18px !important;
            }

            .panel-heading h2 {
                margin: 0;
                font-size: 18px !important;
            }

            .panel-body.text-small span, .panel-body.text-small label {
                font-size: 16px !important;
            }
        }
    </style>


    <!-- iCheck -->
    <script type="text/javascript">
        window.history.forward();
        function noBack() {
            window.history.forward();
        }
    </script>
    <script type="text/javascript">
        function getCheckedRadio() {
            var radioButtons = document.getElementsByName("test");
            for (var x = 0; x < radioButtons.length; x++) {
                if (radioButtons[x].checked) {
                    alert("You checked " + radioButtons[x].id);

                }
            }
        }
     </script>
</head>
<body class="content-wrapper">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager_Main" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>


        <asp:UpdatePanel ID="UpdatePanel_Login" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlCourse" runat="server" Width="100%">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-2">
                                &nbsp;&nbsp;
                            </div>
                            <div class="col-md-8">
                                <div class="panel panel-primary" style="box-shadow: 0 0 30px black; margin-top: 2%;" id="perinfo" runat="server">
                                    <div id="divCollegename" runat="server" class="form-group col-md-12 text-center bg-blue-gradient" style="text-decoration-color: white; font-size: x-large; padding-top: 1%; padding-bottom: 1%; color:#fff;">
                                    </div>
                                    <div class="panel-heading">
                                        <h2 style="font-family: Algerian; text-align: center">Result</h2>
                                    </div>
                                    <div class="panel-body text-small">
                                        <%-- <div class="col-md-12 text-center">
                                       <%-- --<span class="glyphicon glyphicon-bell"></span>
                                        <hr style="border-color: black;" />
                                        <%--<h3 style="font-family: 'Baskerville Old Face'">Your Submission Has Been Received Successfully !!</h3>--
                                    </div>--%>
                                        <div class="form-group col-md-12" style="font-family: 'Baskerville Old Face'; font-size: large;">

                                            <div class="row">
                                             
                                                        <div class="form-group col-md-4 col-xs-6">
                                                            <label>Name <b>:</b></label>
                                                        </div>
                                                        <div class="form-group col-md-8 col-xs-6">
                                                            <asp:Label ID="lblUrname" runat="server" Font-Bold="True"></asp:Label>
                                                        </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-md-4 col-xs-6">
                                                        <label>Session <b>:</b> </label>
                                                    </div>
                                                    <div class="form-group col-md-8 col-xs-6">
                                                        <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-4 col-xs-6">
                                                        <label>Course Name <b>:</b></label>
                                                    </div>
                                                    <div class="form-group col-md-8 col-xs-6">
                                                        <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-md-4 col-xs-6">
                                                        <label>No. of Questions <b>:</b></label>
                                                    </div>
                                                    <div class="form-group col-md-8 col-xs-6">
                                                        <asp:Label ID="lblTotQue" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-md-4 col-xs-6">
                                                        <label>Answered <b>:</b></label>
                                                    </div>
                                                    <div class="form-group col-md-8 col-xs-6">
                                                        <asp:Label ID="lblAnsQue" runat="server" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-md-4 col-xs-6">
                                                        <label>Correct <b>:</b></label>
                                                    </div>
                                                    <div class="form-group col-md-8 col-xs-6" style="color: green">
                                                        <asp:Label ID="lblRightAns" runat="server" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-md-4 col-xs-6">
                                                        <label>Wrong <b>:</b></label>
                                                    </div>
                                                    <div class="form-group col-md-8 col-xs-6" style="color: Red">
                                                        <asp:Label ID="lblWrongAns" runat="server" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-4 col-xs-6">
                                                        <label>Not Answered <b>:</b></label>
                                                    </div>

                                                    <div class="form-group col-md-8 col-xs-6" style="color: Red">
                                                        <asp:Label ID="lblUnAnsQue" runat="server" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-md-4 col-xs-6 bg-maroon-gradient">
                                                        <span><b>Total Score :</b></span>
                                                    </div>
                                                    <div class="form-group col-md-8 col-xs-6 bg-maroon-gradient">
                                                        <asp:Label ID="lblScore" runat="server" Font-Bold="True" Width="232px"></asp:Label>
                                                        <asp:Label ID="lblQueNo" runat="server" Font-Bold="true" Width="232px" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                        
                                        <div class="col-md-12 col-xs-12 text-center">
                                            <hr style="border-color: black" />
                                            <asp:LinkButton ID="btnOK" runat="server" OnClick="btnOK_Click"
                                                OnClientClick="window.close()" Text=""
                                                CssClass="btn btn-success"><i class="fa fa-backward"></i> OK</asp:LinkButton>
                                            <asp:LinkButton ID="btnshowkey" runat="server" Text="" OnClick="btnshowkey_Click" Visible="false"
                                                CssClass="btn btn-success"><i class="fa fa-backward"></i> Show Answer Key</asp:LinkButton>
                                        </div>

                                         

                                </div>
                            </div>

                              
                            <div class="col-md-2">
                                &nbsp;&nbsp;
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                  <asp:Panel ID="divanskye" runat="server" Visible="false">
                      <div class="container">
                        <div class="row">
                            <div class="col-md-2">
                                &nbsp;&nbsp;
                            </div>
                            <div class="col-md-12">
                                <div class="panel panel-primary" style="box-shadow: 0 0 30px black; margin-top: 2%;" id="Div1" runat="server">
                                    
                                    <div class="panel-heading">
                                        <h2 style="font-family: Algerian; text-align: center">Answer Key</h2>
                                    </div>
                                    <div class="panel-body text-small">
                                        
                                        <div class="form-group col-md-12" style="font-family: 'Baskerville Old Face'; font-size: large;">

                                            <asp:Repeater ID="RpCourse" runat="server" OnItemDataBound="RpCourse_ItemDataBound">
                                               <ItemTemplate>
                                                    <div class="col-lg-12 col-md-12 col-12">
                                                       
                                                   <div  class="profesionaldiv" style="box-shadow: rgba(0, 0, 0, 0.2) 1px 5px 5px; padding: 10px; margin-bottom:15px; border-radius: 5px;">
                                                    <table class="tablestyle">
                                                        <tr>
                                                        <th >
                                                            <asp:HiddenField ID="hdnQuestioId" runat="server" Value='<%#Eval("QUESTIONNO")%>' />
                                                       
                                                        </th >
                                                          
                                             
                                         
                                                  <div class="col-12">
                                                   <div class="row">
                                                    <div class="col-md-1">
                                                       <b> <span style="font-size: large;">Que.</span></b>
                                                    </div>
                                              
                                                <div class="col-md-11">
                                             <b> <asp:Label ID="Label2" runat="server" Text="What is Chemistry?"></asp:Label></b>
                                                <asp:Image ID="imgQuesImage" runat="server" CssClass="img-responsive"
                                                    onclick="DisplayAns1InWidnow();" EnableTheming="False" />
                                                </div>
                                            </div>
                                                   
                                                   <th>
                                         
                                            <div class="col-12">
                                        <asp:ListView ID="lvanswer" runat="server" DataKeyNames="answer">
                                                <LayoutTemplate>

                                                    <table  id="">
                                                        <thead >
                                                            <tr>
                                                            <th></th>
                                                            <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>

                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="imgLogo" runat="server" Height="20px" ImageUrl="~/Images/ansbtn.png" />
                                                           <asp:HiddenField  ID="hdrow" runat="server" Value="<%# Container.DataItemIndex + 1%>" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="labelanswer" runat="server" Text='<%# Eval("Answer")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                         
                                            </div>

                                        
        
                                                        </th>
                                                  
                                                        </tr>
                                                    </table>
                                                    
                                                     </div>
                                                             
                                                      </div>
                                                        
                                                    </ItemTemplate>
                                            </asp:Repeater>
                                                      
                                                </div>


                                               <div class="col-md-12 col-xs-12 text-center">
                                            <hr style="border-color: black" />
                                            <asp:LinkButton ID="linkbtnback" runat="server"  OnClick="linkbtnback_Click"
                                                 Text=""
                                                CssClass="btn btn-success"><i class="fa fa-backward"></i> Close</asp:LinkButton>
                                        </div>
                                           
                                        </div>
                                          </div>
                                <div class="col-md-2">
                                &nbsp;&nbsp;
                            </div>
                        </div>
                    </div>
                          </div>
                 </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
