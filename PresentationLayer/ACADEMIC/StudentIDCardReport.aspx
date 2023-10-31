<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentIDCardReport.aspx.cs" Inherits="ACADEMIC_StudentIDCardReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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

    <style>
        /*=========File Upload CSS==========*/
        .logoContainer img
        {
            width: 50px;
            height: 50px;
        }

            .logoContainer img:focus
            {
                color: #495057;
                background-color: #fff;
                border-color: #80bdff;
                outline: 0;
                box-shadow: 0 0 0 0.2rem rgba(0,123,255,.25);
            }

        .fileContainer
        {
            position: relative;
            cursor: pointer;
        }

            .fileContainer span
            {
                overflow: hidden;
                font-weight: bold;
                display: block;
                white-space: nowrap;
                text-overflow: ellipsis;
                cursor: pointer;
            }

            .fileContainer input[type="file"]
            {
                opacity: 0;
                margin: 0;
                padding: 0;
                width: 100%;
                height: 100%;
                left: 0;
                top: 0;
                position: absolute;
                cursor: pointer;
                color: #495057;
            }
        /*=========File Upload end==========*/
    </style>
    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">STUDENT ID CARD</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmission" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClg" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlClg_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClg" runat="server" ControlToValidate="ddlClg" 
                                            Display="None" ErrorMessage="Please Select Institute" SetFocusOnError="true" ValidationGroup="show" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label id="fieldman" runat="server" style="color:red" visible="false">*</label>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show" Visible="false"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">                                          
                                            <%-- <label>Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            
                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup> </sup>
                                            <label>ID Card Format</label>
                                        </div>
                                        &nbsp;&nbsp;<asp:CheckBox ID="chkIDCard" runat="server" Text="Default Format" />
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12 d-none">
                                        <div class="logoContainer">
                                            <img src="~/IMAGES/default-fileupload.png" alt="upload image" imageurl="~/IMAGES/default-fileupload.png" id="imgCollegeLogo" runat="server">
                                        </div>
                                        <div class="fileContainer sprite pl-1">
                                            <span>Upload File</span>
                                            <asp:FileUpload ID="fuRegistrarSign" runat="server" onchange="LoadImage()" CssClass="form-control" TabIndex="1" />
                                            <asp:HiddenField ID="hdnFile" runat="server" />
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="d-none" />
                                        </div>
                                    </div>


                                     <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label> Range From</label>
                                        </div>

                                        <asp:TextBox runat="server" ID="txtRangeFrom" TabIndex="10" CssClass="form-control" ToolTip="Please Enter Range From" MaxLength="5"></asp:TextBox>
                                         <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789." TargetControlID="txtRangeFrom"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Range To</label>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtRangeTo" TabIndex="10" CssClass="form-control" ToolTip="Please Enter Range To" MaxLength="5"></asp:TextBox>
                                         <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789." TargetControlID="txtRangeTo"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Maximum number of generations of ID Cards at a time</label>
                                        </div>
                                        <asp:Image ID="imgCollegeLogo" runat="server" ImageUrl="~/images/nophoto.jpg" BorderColor="#0099FF"
                                            BorderStyle="Solid" BorderWidth="1px" Width="30%" Height="10%"/>
                                        <asp:FileUpload ID="fuRegistrarSign" runat="server" onchange="LoadImage()" />
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click" />
                                    </div>--%>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student" CssClass="btn btn-primary" OnClick="btnShow_Click"
                                    ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" />

                                <asp:Button ID="btnPrintReport" runat="server" Text="Print ID Card" CssClass="btn btn-info"
                                    OnClick="btnPrintReport_Click" ValidationGroup="show" />

                                <asp:Button ID="btnbackReport" runat="server" Text="Print back ID Card" CssClass="btn btn-info"
                                    ValidationGroup="show" OnClick="btnbackReport_Click" Visible="false"  />

                                 <asp:Button ID="btnFrontBackIdentityCard" runat="server" Text="Print Front back ID Card" CssClass="btn btn-info"
                                    ValidationGroup="show" OnClick="btnFrontBackIdentityCard_Click" Visible="false" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" ToolTip="Cancel Selected under Selected Criteria." />

                                <asp:ValidationSummary ID="ValidationsUM" ValidationGroup="show" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" runat="server"/>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                <div class="label-dynamic">
                                    <label>Total Selected</label>
                                </div>
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                    Style="text-align: center;" Font-Bold="True" Font-Size="Small"></asp:TextBox>
                                <%--  Shrink the info panel out of view --%>
                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                    WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                <asp:HiddenField ID="hftot" runat="server" />
                            </div>

                             <div class="form-group col-lg-6 col-md-12 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading">Note</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Maximum 100  ID cards can be genrate  at a time.</span>  </p>
                                    </div>
                                </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvStudentRecords" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">

                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" />
                                                        </th>
                                                        <th>
                                                            Sr.No.
                                                        </th>
                                                        <th><asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th><asp:Label ID="lblDYddlSemester_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
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
                                                    <asp:CheckBox ID="chkReport" runat="server" onClick="totSubjects(this);" />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                     <%# Eval("SRNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function SelectAll(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else
                return true;
        }

        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgCollegeLogo").src = document.getElementById("ctl00_ContentPlaceHolder1_fuCollegeLogo").value;
        }

    </script>
    <%-- file upload script add by arpana --%>
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
    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_fuRegistrarSign").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_fuRegistrarSign").click();
                }
            });
        });
    </script>
</asp:Content>
