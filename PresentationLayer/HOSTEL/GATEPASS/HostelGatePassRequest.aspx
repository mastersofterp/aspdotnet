<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelGatePassRequest.aspx.cs" Inherits="HOSTEL_GATEPASS_HostelGatePassRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style>
            ul.ui-autocomplete
            {
                max-height: 180px !important;
                overflow: auto !important;
            }
        </style>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };

        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;
            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }

        function formatNumber(input) {
            var value = input.value;
            if (value < 10) {
                input.value = '0' + value;
            }
        }
        </script>

    <meta charset="UTF-8">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Hostel Gate Pass Request</h3>
                </div>
                <br /><br /><br />
                <div class="box-body">

                    <div class="row" id ="adminsearch" runat="server" Visible="False">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                                <asp:Label ID="lblSearch" runat="server" Font-Bold="true">Search Student By Name</asp:Label>
                                </div>
                                 <asp:DropDownList ID="ddlSearch" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                     ToolTip="Search Student Name Here" AutoPostBack="True" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged"/>
                                 </div>
                        </div>

                    <div class="col-12" id="pnlStudentHGPRequestDetails" runat="server">
                        
                          <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                           <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblStuType" runat="server" Font-Bold="true">Student Type</asp:Label>
                                           </div>
                                            <asp:DropDownList ID="ddlStuType" runat="server" TabIndex="2" ToolTip="Please Select Student Type." AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvfStuType" runat="server" ControlToValidate="ddlStuType"
                                                Display="None" ErrorMessage="Please Select Student Type." SetFocusOnError="true"
                                                ValidationGroup="submit" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblHostel" runat="server" Font-Bold="true">Hostel</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlHostel" runat="server" TabIndex="3" ToolTip="Please Select Hostel." AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                                                Display="None" ErrorMessage="Please Select Hostel." SetFocusOnError="true"
                                                ValidationGroup="submit" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-4 col-md-6 col-12" style="border:dashed; border-width:1px; height:50%" hidden="hidden">
                                <p id="path" runat="server" style="text-align:center; padding:1%;">Please Select Student Type.</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>OUT Date </label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="imgoutDate" runat="server" class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtoutDate" runat="server" TabIndex="4" CssClass="form-control" AutoPostBack="true" ValidationGroup="submit" OnTextChanged="txtoutDate_TextChanged"
                                        />
                                    <ajaxToolKit:CalendarExtender ID="ceoutDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtoutDate" PopupButtonID="txtoutDate" />
                                    <ajaxToolKit:MaskedEditExtender ID="meoutDate" runat="server" TargetControlID="txtoutDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" MaskType="Date" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvoutDate" runat="server" EmptyValueMessage="Please Select Out Date"
                                        ControlExtender="meoutDate" ControlToValidate="txtoutDate" IsValidEmpty="false"
                                        InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Select Date"
                                        InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                                    <asp:CompareValidator ID="cvoutDate" runat="server" ControlToValidate="txtoutDate"
                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter a valid out date mm/dd/yyyy)."
                                        EnableClientScript="False" ValidationGroup="submit">
                                    </asp:CompareValidator>
                                </div>
                            </div>
                            <div class="form-group col-lg-1 col-md-4 col-12">
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hour From</label>
                                </div>
                                <%--<asp:TextBox ID="txtoutHourFrom" oninput="formatNumber(this)" CssClass="form-control" runat="server" TabIndex="5" TextMode="Number" Min="1" Max="12"/>--%>
                                <asp:DropDownList ID="ddloutHourFrom" AppendDataBoundItems="true" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0" Selected="True">00</asp:ListItem>
                                    <asp:ListItem Value="1" >01</asp:ListItem>
                                    <asp:ListItem Value="2">02</asp:ListItem>
                                    <asp:ListItem Value="3">03</asp:ListItem>
                                    <asp:ListItem Value="4">04</asp:ListItem>
                                    <asp:ListItem Value="5">05</asp:ListItem>
                                    <asp:ListItem Value="6">06</asp:ListItem>
                                    <asp:ListItem Value="7">07</asp:ListItem>
                                    <asp:ListItem Value="8">08</asp:ListItem>
                                    <asp:ListItem Value="9">09</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvoutHourFrom" Display="None" runat="server" ErrorMessage="Please Select Out Hour From" ControlToValidate="ddloutHourFrom" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Minutes From</label>
                                </div>
                                <%--<asp:TextBox ID="txtoutMinFrom" oninput="formatNumber(this)" CssClass="form-control" runat="server" TabIndex="6" TextMode="Number" Min="0" Max="60"/>--%>
                                <asp:DropDownList ID="ddloutMinFrom" AppendDataBoundItems="true" runat="server" TabIndex="6" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0" Selected="True">00</asp:ListItem>
                                    <asp:ListItem Value="1">01</asp:ListItem>
                                    <asp:ListItem Value="2">02</asp:ListItem>
                                    <asp:ListItem Value="3">03</asp:ListItem>
                                    <asp:ListItem Value="4">04</asp:ListItem>
                                    <asp:ListItem Value="5">05</asp:ListItem>
                                    <asp:ListItem Value="6">06</asp:ListItem>
                                    <asp:ListItem Value="7">07</asp:ListItem>
                                    <asp:ListItem Value="8">08</asp:ListItem>
                                    <asp:ListItem Value="9">09</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="13">13</asp:ListItem>
                                    <asp:ListItem Value="14">14</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="16">16</asp:ListItem>
                                    <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="19">19</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="21">21</asp:ListItem>
                                    <asp:ListItem Value="22">22</asp:ListItem>
                                    <asp:ListItem Value="23">23</asp:ListItem>
                                    <asp:ListItem Value="24">24</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="26">26</asp:ListItem>
                                    <asp:ListItem Value="27">27</asp:ListItem>
                                    <asp:ListItem Value="28">28</asp:ListItem>
                                    <asp:ListItem Value="29">29</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="31">31</asp:ListItem>
                                    <asp:ListItem Value="32">32</asp:ListItem>
                                    <asp:ListItem Value="33">33</asp:ListItem>
                                    <asp:ListItem Value="34">34</asp:ListItem>
                                    <asp:ListItem Value="35">35</asp:ListItem>
                                    <asp:ListItem Value="36">36</asp:ListItem>
                                    <asp:ListItem Value="37">37</asp:ListItem>
                                    <asp:ListItem Value="38">38</asp:ListItem>
                                    <asp:ListItem Value="39">39</asp:ListItem>
                                    <asp:ListItem Value="40">40</asp:ListItem>
                                    <asp:ListItem Value="41">41</asp:ListItem>
                                    <asp:ListItem Value="42">42</asp:ListItem>
                                    <asp:ListItem Value="43">43</asp:ListItem>
                                    <asp:ListItem Value="44">44</asp:ListItem>
                                    <asp:ListItem Value="45">45</asp:ListItem>
                                    <asp:ListItem Value="46">46</asp:ListItem>
                                    <asp:ListItem Value="47">47</asp:ListItem>
                                    <asp:ListItem Value="48">48</asp:ListItem>
                                    <asp:ListItem Value="49">49</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="51">51</asp:ListItem>
                                    <asp:ListItem Value="52">52</asp:ListItem>
                                    <asp:ListItem Value="53">53</asp:ListItem>
                                    <asp:ListItem Value="54">54</asp:ListItem>
                                    <asp:ListItem Value="55">55</asp:ListItem>
                                    <asp:ListItem Value="56">56</asp:ListItem>
                                    <asp:ListItem Value="57">57</asp:ListItem>
                                    <asp:ListItem Value="58">58</asp:ListItem>
                                    <asp:ListItem Value="59">59</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvoutMinFrom" Display="None" runat="server" ErrorMessage="Please Select Out Minutes From" ControlToValidate="ddloutMinFrom" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>AM/PM</label>
                                </div>
                                <asp:DropDownList ID="ddlAM_PM1" AppendDataBoundItems="true" runat="server" TabIndex="7" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="AM">AM</asp:ListItem>
                                    <asp:ListItem Value="PM">PM</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDropDownList1" Display="None" runat="server" ErrorMessage="Please Select AM/PM For Out Date" ControlToValidate="ddlAM_PM1" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>IN Date </label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="imginDate" runat="server" class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtinDate" runat="server" TabIndex="8" CssClass="form-control" AutoPostBack="true" ValidationGroup="submit" OnTextChanged="txtinDate_TextChanged"
                                        />
                                    <ajaxToolKit:CalendarExtender ID="ceinDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtinDate" PopupButtonID="txtinDate" />
                                    <ajaxToolKit:MaskedEditExtender ID="meinDate" runat="server" TargetControlID="txtinDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" MaskType="Date" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvinDate" runat="server" EmptyValueMessage="Please Select In Date"
                                        ControlExtender="meinDate" ControlToValidate="txtinDate" IsValidEmpty="false"
                                        InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Select Date"
                                        InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                                    <asp:CompareValidator ID="cvinDate" runat="server" ControlToValidate="txtinDate"
                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter a valid in date mm/dd/yyyy)."
                                        EnableClientScript="False" ValidationGroup="submit">
                                    </asp:CompareValidator>
                                </div>
                            </div>
                            <div class="form-group col-lg-1 col-md-4 col-12">
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hour To</label>
                                </div>
                                <%--<asp:TextBox ID="txtinHourFrom" oninput="formatNumber(this)" CssClass="form-control" runat="server" TabIndex="9" TextMode="Number" Min="1" Max="12"/>--%>
                                <asp:DropDownList ID="ddlinHourFrom" AppendDataBoundItems="true" runat="server" TabIndex="9" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlinHourFrom_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">00</asp:ListItem>
                                    <asp:ListItem Value="1">01</asp:ListItem>
                                    <asp:ListItem Value="2">02</asp:ListItem>
                                    <asp:ListItem Value="3">03</asp:ListItem>
                                    <asp:ListItem Value="4">04</asp:ListItem>
                                    <asp:ListItem Value="5">05</asp:ListItem>
                                    <asp:ListItem Value="6">06</asp:ListItem>
                                    <asp:ListItem Value="7">07</asp:ListItem>
                                    <asp:ListItem Value="8">08</asp:ListItem>
                                    <asp:ListItem Value="9">09</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvinHourFrom" Display="None" runat="server" ErrorMessage="Please Select In Hour From" ControlToValidate="ddlinHourFrom" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Minutes To</label>
                                </div>
                                <%--<asp:TextBox ID="txtinMinFrom" oninput="formatNumber(this)" CssClass="form-control" runat="server" TabIndex="10" TextMode="Number" Min="0" Max="60"/>--%>
                                <asp:DropDownList ID="ddlinMinFrom" AppendDataBoundItems="true" runat="server" TabIndex="10" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0" Selected="True">00</asp:ListItem>
                                    <asp:ListItem Value="1">01</asp:ListItem>
                                    <asp:ListItem Value="2">02</asp:ListItem>
                                    <asp:ListItem Value="3">03</asp:ListItem>
                                    <asp:ListItem Value="4">04</asp:ListItem>
                                    <asp:ListItem Value="5">05</asp:ListItem>
                                    <asp:ListItem Value="6">06</asp:ListItem>
                                    <asp:ListItem Value="7">07</asp:ListItem>
                                    <asp:ListItem Value="8">08</asp:ListItem>
                                    <asp:ListItem Value="9">09</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="13">13</asp:ListItem>
                                    <asp:ListItem Value="14">14</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="16">16</asp:ListItem>
                                    <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="19">19</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="21">21</asp:ListItem>
                                    <asp:ListItem Value="22">22</asp:ListItem>
                                    <asp:ListItem Value="23">23</asp:ListItem>
                                    <asp:ListItem Value="24">24</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="26">26</asp:ListItem>
                                    <asp:ListItem Value="27">27</asp:ListItem>
                                    <asp:ListItem Value="28">28</asp:ListItem>
                                    <asp:ListItem Value="29">29</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="31">31</asp:ListItem>
                                    <asp:ListItem Value="32">32</asp:ListItem>
                                    <asp:ListItem Value="33">33</asp:ListItem>
                                    <asp:ListItem Value="34">34</asp:ListItem>
                                    <asp:ListItem Value="35">35</asp:ListItem>
                                    <asp:ListItem Value="36">36</asp:ListItem>
                                    <asp:ListItem Value="37">37</asp:ListItem>
                                    <asp:ListItem Value="38">38</asp:ListItem>
                                    <asp:ListItem Value="39">39</asp:ListItem>
                                    <asp:ListItem Value="40">40</asp:ListItem>
                                    <asp:ListItem Value="41">41</asp:ListItem>
                                    <asp:ListItem Value="42">42</asp:ListItem>
                                    <asp:ListItem Value="43">43</asp:ListItem>
                                    <asp:ListItem Value="44">44</asp:ListItem>
                                    <asp:ListItem Value="45">45</asp:ListItem>
                                    <asp:ListItem Value="46">46</asp:ListItem>
                                    <asp:ListItem Value="47">47</asp:ListItem>
                                    <asp:ListItem Value="48">48</asp:ListItem>
                                    <asp:ListItem Value="49">49</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="51">51</asp:ListItem>
                                    <asp:ListItem Value="52">52</asp:ListItem>
                                    <asp:ListItem Value="53">53</asp:ListItem>
                                    <asp:ListItem Value="54">54</asp:ListItem>
                                    <asp:ListItem Value="55">55</asp:ListItem>
                                    <asp:ListItem Value="56">56</asp:ListItem>
                                    <asp:ListItem Value="57">57</asp:ListItem>
                                    <asp:ListItem Value="58">58</asp:ListItem>
                                    <asp:ListItem Value="59">59</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvinMinFrom" Display="None" runat="server" ErrorMessage="Please Select In Minutes From" ControlToValidate="ddlinMinFrom" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>AM/PM</label>
                                </div>
                                <asp:DropDownList ID="ddlAM_PM2" AppendDataBoundItems="true" runat="server" TabIndex="11" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAM_PM2_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="AM">AM</asp:ListItem>
                                    <asp:ListItem Value="PM">PM</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDropDownList2" Display="None" runat="server" ErrorMessage="Please Select AM/PM For In Date" ControlToValidate="ddlAM_PM2" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                         <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Purpose</label>
                                </div>
                                <asp:DropDownList ID="ddlPurpose" AppendDataBoundItems="true" runat="server" TabIndex="12" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlPurpose_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPurpose" Display="None" runat="server" ErrorMessage="Please Select Purpose" ControlToValidate="ddlPurpose" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                         </div>
                        </div>
                        <div class="row">
                        <div class="form-group col-lg-8 col-md-4 col-12">
                                <asp:TextBox ID="txtOther" runat="server" CssClass="form-control" TabIndex="13" Visible="False" PlaceHolder="Enter Your Purpose" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOther" runat="server" ErrorMessage="Please Enter Other Purpose"
                                    Display="None" ControlToValidate="txtOther" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                         </div>
                        </div>

                        <div class="row">
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Remark</label>
                                </div>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="14" Rows="1" Height="74px"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvtxtRemark" runat="server" ErrorMessage="Please Enter Remark"
                                    Display="None" ControlToValidate="txtRemark" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                         </div>
                        </div>
                    </div>
                    <br /><br />
                    <div class="col-12 btn-footer" id="pnlbuttons" runat="server">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" TabIndex="15"
                            CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="16"
                             CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>

                    <div class="col-12">
                        <asp:Repeater ID="lvGatePass" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Hostel Purposes</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit
                                            </th>
                                            <th>Student Name
                                            </th>
                                            <th>Out Date
                                            </th>
                                            <th>In Date
                                            </th>
                                            <th>Purpose
                                            </th>
                                            <th>Remarks
                                            </th>
                                            <th>Gate Pass No
                                            </th>
                                            <th>
                                                Status
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("HGP_ID") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="17" />&nbsp;
                                    </td>
                                    <td>
                                         <%# Eval("STUDNAME") %>
                                        <%--<asp:HiddenField ID="hdnIdno" runat="server" Value='<%# Eval("IDNO") %>' />
                                        <asp:HiddenField ID="hdnHgpId" runat="server" Value='<%# Eval("HGP_ID") %>' />
                                        <asp:HiddenField ID="hdnDeegreeno" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                        <asp:HiddenField ID="hdnHostelno" runat="server" Value='<%# Eval("HOSTEL_NO") %>' />
                                        <asp:HiddenField ID="hdnStutype" runat="server" Value='<%# Eval("STUDENT_TYPE") %>' />--%>
                                    </td>
                                    <td>
                                        <%# Eval("OUTDATE ") %>
                                    </td>
                                    <td>
                                        <%# Eval("INDATE ") %>
                                    </td>
                                    <td>
                                        <%# Eval("PURPOSE_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("REMARKS") %>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGatepassnno" runat="server" Text='<%# (Eval("HOSTEL_GATE_PASS_NO").ToString())=="" ? "..." : Eval("HOSTEL_GATE_PASS_NO") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblstatus" runat="server" ForeColor='<%# Eval("STATUS").Equals("APPROVED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("STATUS") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .form-control {}
    </style>
</asp:Content>



