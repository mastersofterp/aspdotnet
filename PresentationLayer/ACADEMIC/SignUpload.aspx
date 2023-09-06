<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SignUpload.aspx.cs" Inherits="ACADEMIC_SignUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--END : FOLLOWING CODE ALLOWS THE AUTOCOMPLETE TO BE FIRED IN UPDATEPANEL--%>
    <%--<style type="text/css">
        .css3gradient
        {
            background: -moz-linear-gradient(top,rgb(166,199,76) 46%, rgb(166,199,76) 84%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(46%,rgba(32,89,146,1)), color-stop(84%,rgb(166,199,76)));
            background: -webkit-linear-gradient(top,rgb(166,199,76) 46%,rgb(166,199,76) 84%);
            background-color: rgba(0, 0, 0, 0);
            background: -o-linear-gradient(top,rgb(166,199,76) 46%,rgb(166,199,76) 84%);
            background: -ms-linear-gradient(top,rgb(166,199,76) 46%,rgb(166,199,76) 84%);
            /*background: linear-gradient(to bottom,rgb(166,199,76) 46%,rgb(166,199,76) 84%);*/
            background: linear-gradient(to bottom,rgb(25, 118, 210) 46%,rgb(96, 189, 242) 84%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#A6C74C', endColorstr='#A6C74C',GradientType=0 );
            color: #fff;
        }
    </style>--%>


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

    <script type="text/javascript">
        function previewFilePhoto() {
            //ADDED BY MD. REHBAR SHEIKH 
            if (validateFileSize()) {



                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {

                    reader.readAsDataURL(file);

                } else {
                    preview.src = "";
                }
            }
            else {
                alert('File size must be upto 150KB. If your file size more than 150KB then you should try to crop image.');

                $('[id*=pnlCrop]').show();
                $('[id*=pnlNonCrop]').hide();
                $('#<%= rdoCrop.ClientID %>').prop('checked', true);

                //***********20-03-18 ADDED BY: MD. REHBAR SHEIKH*********

                //debugger;


                //$('#Image1').hide();

                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    $('#Image1').show();
                    $('#Image1').attr("src", e.target.result);
                    $('#Image1').Jcrop({
                        onChange: SetCoordinates,
                        onSelect: SetCoordinates
                    });
                }
                //reader.readAsDataURL($(this)[0].files[0]);
                reader.readAsDataURL($(this).get(0).files[0]);


                function SetCoordinates(c) {
                    $('#imgX1').val(c.x);
                    $('#imgY1').val(c.y);
                    $('#imgWidth').val(c.w);
                    $('#imgHeight').val(c.h);
                    $('#btnCrop').show();
                };
                //***********************

                return;
            }
        }


        function previewFileSignature() {
            //ADDED BY MD. REHBAR SHEIKH 
            if (validateFileSizeSign()) {

                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }

            }
            else {
                alert('File size must be upto 150KB. If your file size more than 150KB then you should try to crop image.');
                $('[id*=pnlCrop]').show();
                $('[id*=pnlNonCrop]').hide();


                //*************20-03-18 ADDED BY: MD. REHBAR SHEIKH*************


                // $('#Image2').show();

                var reader1 = new FileReader();
                reader1.readAsDataURL(file1);
                reader1.onload = function (e) {
                    $('#Image2').show();
                    $('#Image2').attr("src", e.target.result);
                    $('#Image2').Jcrop({
                        onChange: SetCoordinates1,
                        onSelect: SetCoordinates1
                    });
                }
                //reader1.readAsDataURL($(this)[0].files[0]);
                reader1.readAsDataURL($(this).get(0).files[0]);

                function SetCoordinates1(c) {
                    $('#img1X1').val(c.x);
                    $('#img1Y1').val(c.y);
                    $('#img1Width').val(c.w);
                    $('#img1Height').val(c.h);
                    $('#btnCropSign').show();
                };
                //******************************

                return;
            }
        }


        //ADDED BY MD. REHBAR SHEIKH 
        function validateFileSize() {

            if (uploadControl.files[0].size > 153600) {
                return false;
            }
            else {
                return true;
            }
        }
        //ADDED BY MD. REHBAR SHEIKH 
        function validateFileSizeSign() {

            if (uploadControl.files[0].size > 153600) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpersonalinformation"
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

    <asp:UpdatePanel ID="updpersonalinformation" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>

                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session List</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: green; font-weight: bold">Only JPG,JPEG,PNG files are allowed upto 150 KB size For Signature</span>  </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Please select authority</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Authority List</label>
                                        </div>
                                        <asp:RadioButtonList ID="rblAthority" runat="server" AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="rblAthority_SelectedIndexChanged">
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Authority" ControlToValidate="rblAthority" Display="None" ValidationGroup="Academic" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Signature</label>
                                        </div>

                                        <div id="Div3" class="logoContainer" runat="server">
                                            <img src="../Images/default-fileupload.png" alt="upload image" runat="server" id="imgUpFile" tabindex="6" />

                                        </div>
                                        <div class="fileContainer sprite pl-1">
                                            <span runat="server" id="ufFile"
                                                cssclass="form-control" tabindex="7">Upload File</span>
                                            <asp:FileUpload ID="fuSignUpload" runat="server" onChange="previewFilePhoto2()"
                                                CssClass="form-control" accept=".jpg,.jpeg,.JPG,.JPEG,.PNG" onkeypress="" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Upload Signature" ControlToValidate="fuSignUpload" Display="None" ValidationGroup="Academic" />
                                            <asp:Button ID="btnSignUpload" CssClass="btn btn-primary d-none" runat="server" Text="Upload" Visible="false" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="3" Text="Submit" ToolTip="Click to Submit"
                                    class="btn btn-primary" ValidationGroup="Academic" onChange="previewFilePhoto2()" OnClick="btnSubmit_Click" />
                                <%-- <asp:Button ID="btnGohome" runat="server" TabIndex="39" Text="Go Back Home" ToolTip="Click to Go Back Home"
                                    class="btn btn-warning btnGohome" UseSubmitBehavior="false" OnClick="btnGohome_Click" />--%>
                                <button runat="server" id="btnGohome" visible="false" tabindex="39" class="btn btn-primary btnGohome" tooltip="Click to Go Back Home">
                                    <i class="fa fa-home"></i>Go Back Home
                                </button>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Academic" />
                            </div>

                            <div style="text-align: center; padding-top: 1%; padding-bottom: 1%; display: none">
                                <asp:RadioButton ID="rdoNoCrop" GroupName="crop" Text="Without Croping" runat="server" Checked="true" />
                                <asp:RadioButton ID="rdoCrop" GroupName="crop" Text="Croping Image" runat="server" />
                                <%--<asp:RadioButton ID="rdoBack" GroupName="crop" Text="Back" runat="server"  />--%>
                            </div>
                        </div>


                        <%--<asp:Image ID="ImgSign" runat="server" Width="130px" Height="55px" />
                                    <asp:Image ID="Image2" runat="server" Width="130px" Height="55px" Visible="false" style="margin-left:60px;"/>
                                <asp:FileUpload ID="fuSignUpload" runat="server" onChange="previewFilePhoto2()" TabIndex="2"  />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                                ErrorMessage="Please Upload Signature" ControlToValidate="fuSignUpload"
                            Display="None" ValidationGroup="Academic" />--%>

                        <%--<asp:Button ID="btnSignUpload" CssClass="btn btn-primary" runat="server" Text="Upload"   Visible="false" />
                                <asp:Image ID="Image1" runat="server"  />--%>

                        <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="athority" HeaderText="Authority" />
                                                                
                                        <asp:ImageField DataImageUrlField="signature" DataImageUrlFormatString="~/showimage.aspx" HeaderText="image">
                                        </asp:ImageField>
                                                                
                                    </Columns>
                                    </asp:GridView>--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <%-- <asp:PostBackTrigger ControlID="btnPhotoUpload" />--%>
            <asp:PostBackTrigger ControlID="btnSignUpload" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuPhotoUpload").value;
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").height = '96px';
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").width = '96px';
        }
        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }


        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.focus();
                alert('Only Alphabets Allowed');
                return false;
            }
            else
                return true;

        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>
    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <%-- file upload script add by arpana --%>

    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_fuSignUpload").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_fuSignUpload").click();
                }
            });
        });
    </script>

    <script>
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
    </script>

    <%--<script type="text/javascript">
       

        function previewFilePhoto2() {
            //var preview = document.querySelector('#<%=ImgSign.ClientID %>');
            var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>--%>
    <%--<script type="text/javascript">
      
            function previewFilePhoto2() {
                var preview = document.querySelector('#<%=ImgSign.ClientID %>');
                var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
            }


        
    </script>--%>
    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

    <style id="cssStyle" type="text/css" media="all">
        .CS {
            background-color: #abcdef;
            color: Yellow;
            border: 1px solid #AB00CC;
            font: Verdana 10px;
            padding: 1px 4px;
            font-family: Palatino Linotype, Arial, Helvetica, sans-serif;
        }
    </style>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">
        //ADDED BY: MD. REHBAR SHEIKH ON 27-02-2018

        $(document).ready(function () {
            $('#<%= rdoCrop.ClientID %>').change(function () {
                $('[id*=pnlCrop]').show();
                $('[id*=pnlNonCrop]').hide();
            });

            $('#<%= rdoNoCrop.ClientID %>').change(function () {
                //alert('rdo no crop');
                $('[id*=pnlNonCrop]').show();
                $('[id*=pnlCrop]').hide();
            });

        });
    </script>

</asp:Content>
