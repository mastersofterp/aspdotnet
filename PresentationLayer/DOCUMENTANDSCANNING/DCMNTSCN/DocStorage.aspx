<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DocStorage.aspx.cs" Inherits="DOCUMENTANDSCANNING_DCMNTSCN_DocType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                    <h3 class="box-title">DOCUMENT STORAGE </h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDoctype" runat="server">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Document Type </label>
                                    </div>
                                    <asp:DropDownList ID="ddldoctype" runat="server" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                        CssClass="form-control" ToolTip="Select Parent Document Type" TabIndex="1" OnSelectedIndexChanged="ddldoctype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="ddldoctype" Display="None" InitialValue="0"
                                        ErrorMessage="Please select Document Type" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>


                              <%--  <div class="form-group col-lg-3 col-md-6 col-12" id="divDoctypedatas" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Document Type</label>
                                    </div>
                                    <asp:TextBox ID="txtdoctype" runat="server" CssClass="form-control" ToolTip="Enter Document Type" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtdoctype" Display="None"
                                        ErrorMessage="Please Enter Document Type" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                     <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtdoctype"
                                       FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/ ">
                                      </ajaxToolKit:FilteredTextBoxExtender>
                                </div>--%>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Divdate" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="ImaCalStartDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtDate" runat="server" ToolTip="Enter Date" CssClass="form-control" TabIndex="2"></asp:TextBox>

                                        <ajaxToolKit:CalendarExtender ID="cetxtDate" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <%-- <asp:RequiredFieldValidator ID="rfvtxtDate" runat="server" ControlToValidate="txtDate"
                                            Display="None" ErrorMessage="Please Select Date in (dd/MM/yyyy Format)"
                                            SetFocusOnError="True" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>--%>
                                        <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" TargetControlID="txtDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="meDate"
                                            ControlToValidate="txtDate" EmptyValueMessage="Please Date"
                                            InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Submit" SetFocusOnError="True" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDocNo" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Document Number</label>
                                    </div>
                                    <asp:TextBox ID="txtdocnumber" runat="server" CssClass="form-control" ToolTip="Enter Document Number" TabIndex="3" MaxLength="15" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvdocnumber" runat="server"
                                        ControlToValidate="txtdocnumber" Display="None"
                                        ErrorMessage="Please Enter Document Number" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                  <%--  <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtdocnumber"
                                       FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/ ">
                                      </ajaxToolKit:FilteredTextBoxExtender>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAddress" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Property Address</label>
                                    </div>
                                    <asp:TextBox ID="txtpropAddress" runat="server" CssClass="form-control" ToolTip="Enter Address" TextMode="MultiLine" TabIndex="4" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtpropAddress" Display="None"
                                        ErrorMessage="Please Enter Property Address" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                       <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtpropAddress"
                                       FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/ ">
                                      </ajaxToolKit:FilteredTextBoxExtender>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDistrict" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>District / Taluka / Village</label>
                                    </div>
                                    <asp:TextBox ID="txtdistrict" runat="server" CssClass="form-control" ToolTip="Enter District" TabIndex="5" MaxLength="15"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtdocnumber" Display="None"
                                        ErrorMessage="Please Enter District\Taluka\Village" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtdistrict"
                                       FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/ ">
                                      </ajaxToolKit:FilteredTextBoxExtender>
                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12" id="DivSurveyNo" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Survey Number</label>
                                    </div>
                                    <asp:TextBox ID="txtsurveyNumber" runat="server" CssClass="form-control" ToolTip="Enter Survey Number" TabIndex="6" MaxLength="15"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtsurveyNumber" Display="None"
                                        ErrorMessage="Please Enter Survey Number" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                      <%--  <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtsurveyNumber"
                                       FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/ ">
                                      </ajaxToolKit:FilteredTextBoxExtender>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDivisionNo" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Sub Division Number</label>
                                    </div>
                                    <asp:TextBox ID="txtsubdivnum" runat="server" CssClass="form-control" ToolTip="Enter Sub Division Number" TabIndex="7" MaxLength="15"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtsubdivnum" Display="None"
                                        ErrorMessage="Please Enter sub Division Number" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                       <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtsubdivnum"
                                       FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/ ">
                                      </ajaxToolKit:FilteredTextBoxExtender>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divArea" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Total Sq Ft Area</label>
                                    </div>
                                    <asp:TextBox ID="txtarea" runat="server" CssClass="form-control" ToolTip="Enter Total Area" TabIndex="8" oninput="validateNumberInput(this)" MaxLength="15" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="txtarea" Display="None"
                                        ErrorMessage="Please Enter Total Area" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>  
                                </div>



                                <div class="form-group col-lg-3 col-md-6 col-12" id="divnorth" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>North-Sq. Ft.</label>
                                    </div>
                                    <asp:TextBox ID="txtnorth" runat="server" CssClass="form-control" ToolTip="Enter North-Sq. Ft." TabIndex="9" oninput="validateNumberInput(this)" MaxLength="15"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvnorth" runat="server"
                                        ControlToValidate="txtnorth" Display="None"
                                        ErrorMessage="Please Enter North-Sq Ft." SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                       <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtnorth"
                                       FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/">
                                      </ajaxToolKit:FilteredTextBoxExtender>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divsouth" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>South-Sq. Ft.</label>  
                                    </div>
                                    <asp:TextBox ID="txtSouth" runat="server" CssClass="form-control" ToolTip="Enter South-Sq. Ft." TabIndex="10" oninput="validateNumberInput(this)" MaxLength="15" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvsouth" runat="server"
                                        ControlToValidate="txtSouth" Display="None"
                                        ErrorMessage="Please Enter South-Sq Ft." SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divEast" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>East-Sq. Ft.</label>
                                    </div>
                                    <asp:TextBox ID="txtEast" runat="server" CssClass="form-control" ToolTip="Enter East-Sq. Ft." TabIndex="11" oninput="validateNumberInput(this)" MaxLength="15"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEast" runat="server"
                                        ControlToValidate="txtEast" Display="None"
                                        ErrorMessage="Please Enter East-Sq Ft." SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divWest" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>West-Sq. Ft.</label>
                                    </div>
                                    <asp:TextBox ID="txtWest" runat="server" CssClass="form-control" ToolTip="Enter West-Sq. Ft." TabIndex="12" oninput="validateNumberInput(this)" MaxLength="15"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvWest" runat="server"
                                        ControlToValidate="txtWest" Display="None"
                                        ErrorMessage="Please Enter West-Sq Ft." SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divEcNo" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>EC Number</label>
                                    </div>
                                    <asp:TextBox ID="txtEC" runat="server" CssClass="form-control" ToolTip="Enter EC Number" TabIndex="13" MaxLength="15"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEC" Display="None" ErrorMessage="Please Enter EC NO." ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtEC"
                                       FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/ ">
                                      </ajaxToolKit:FilteredTextBoxExtender>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divFDate" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image3">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <span id="lblSpan" runat="server" style="text-align: left; display: none"></span>
                                        <asp:TextBox ID="txtFromDate" runat="server" Style="text-align: left" TabIndex="14"
                                            CssClass="form-control" />
                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            PopupButtonID="Image3" TargetControlID="txtFromDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                            OnInvalidCssClass="errordate" TargetControlID="txtFromDate" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <asp:RequiredFieldValidator ID="rfvfdate" runat="server" ControlToValidate="txtFromDate" Display="None" ErrorMessage="Please Enter From_Date" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divTDate" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgToDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                      <%--  <asp:TextBox ID="txttodate" runat="server" ToolTip="Enter To Date" CssClass="form-control" TabIndex="3" Text="" OnTextChanged="txttodate_TextChanged"></asp:TextBox>--%>
                                        <asp:TextBox ID="txttodate" runat="server" Style="text-align: left"
                                            CssClass="form-control" OnTextChanged="txttodate_TextChanged" AutoPostBack="true" />
                                        <%-- <div class="input-group-addon">
                                                                    <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />
                                                                </div>--%>
                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                            Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txttodate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                            PopupPosition="BottomRight" TargetControlID="txttodate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meToDate" ControlToValidate="txttodate"
                                            EmptyValueMessage="Please Select To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                            ValidationGroup="Store" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>
                                         <asp:RequiredFieldValidator ID="rfvtdate" runat="server" ControlToValidate="txttodate" Display="None" ErrorMessage="Please Enter To_Date" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>


                                
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divAttach" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label><sup>*</sup>Attachment <small style="color: blue;">(Max.Size
                                        <asp:Label ID="lblFileSize" runat="server" Font-Bold="true"> 10MB</asp:Label>)  (File Ext.
                                          <asp:Label ID="lblExtension" runat="server" Font-Bold="true"></asp:Label>)</small></label>      
                                        </div>
                                        <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="16" />
                                        <asp:Label ID="lblFileName" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-1 btn-footer" id="divbtnAdd" runat="server" visible="false">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" TabIndex="17" CssClass="btn btn-primary" OnClick="btnAdd_Click" ToolTip="Attach File" />
                                    </div>
                                
                            </div>
                        </asp:Panel>

                            <div id="divAttch" runat="server" style="display: none">
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-6">
                                    <asp:Panel ID="pnlAttachmentList" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvCompAttach" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th id="divattach" runat="server">Attachments  
                                                            </th>
                                                            <th id="divattachblob" runat="server" visible="false">Attachments
                                                            </th>
                                                            <th id="divDownload" runat="server" visible="false">Download
                                                            </th>
                                                            <th id="divBlobDownload" runat="server" visible="false">Download
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
                                                        <asp:LinkButton ID="lnkRemoveAttach" runat="server" CommandArgument='<%# Eval("SR_NO")%>'
                                                            OnClick="lnkRemoveAttach_Click" CssClass="mail_pg">Remove</asp:LinkButton>

                                                        <ajaxToolKit:ConfirmButtonExtender ID="CnfDrop" runat="server"
                                                            ConfirmText="Are you Sure, Want to Remove.?" TargetControlID="lnkRemoveAttach">
                                                        </ajaxToolKit:ConfirmButtonExtender>
                                                    </td>
                                                    <td id="attachfile" runat="server">
                                                        <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILE_PATH"))%>'><%# Eval("FILE_NAME")%></asp:HyperLink>
                                                        --%>

                                                        <img alt="Attachment" src="../../Images/attachment.png" />
                                                        <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                            <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                    </td>
                                                    <td id="attachblob" runat="server" visible="false">
                                                        <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILE_PATH"))%>'><%# Eval("FILE_NAME")%></asp:HyperLink>
                                                        --%>

                                                        <img alt="Attachment" src="../../Images/attachment.png" />
                                                        <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                        --%>      <%# Eval("FILE_PATH")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                    </td>


                                                    <td id="tdDownloadLink" runat="server" visible="false">


                                                        <img alt="Attachment" src="../../Images/attachment.png" />
                                                        <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                        --%>      <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                            
                                                    </td>

                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILE_NAME") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("FILE_NAME") %>' Visible='<%# Convert.ToString(Eval("FILE_NAME"))==string.Empty?false:true %>'></asp:ImageButton>

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

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
                    <div class="col-12 btn-footer">

                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="18"
                            CssClass="btn btn-primary" OnClick="btnSubmit_Click" ToolTip="Click here to Submit" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" 
                            ToolTip="Click here to Reset" TabIndex="19" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>


                                         <div class="col-12 table-responsive">
                            <asp:ListView ID="lvDocStorage" runat="server" OnItemDataBound="lvDocStorage_ItemDataBound">
                                <EmptyDataTemplate>
                                    <center>
                                 <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" ></asp:Label>
                                                        </center>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Document Details </h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                  <th>
                                                      Document Type
                                                  </th>
                                                    <th id="thDocNo" runat="server">
                                                    Document No.
                                                    </th>
                                                    <th id="thDistrict" runat="server" >
                                                    District
                                                    </th>
                                                    <th id="thSurNo" runat="server">
                                                     Survey No.
                                                    </th>
                                                        <th id="thECNo" runat="server">
                                                      EC No.
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
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnEditDoc" runat="server"  CommandArgument='<%# Eval("DOCID") %>'
                                                ImageUrl="~/Images/edit.png" ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEditDoc_Click" />
                                            <%--<asp:ImageButton ID="btnDeleteDoc" runat="server"  CommandArgument='<%# Eval("DOCID") %>'
                                                ImageUrl="~/Images/delete.png" ToolTip="Delete Record" AlternateText="Delete Record"
                                                OnClientClick="return confirm('Are You Sure You Want To Delete This Record?');" OnClick="btnDeleteDoc_Click" />--%>

                                        </td>
                                        <td>
                                            <%#Eval("DOCUMENT_TYPE") %>
                                        </td>
                                           <td id="tdDocNo" runat="server">
                                            <asp:Label ID="lblDno" runat="server" Text='<%# Eval("DNO")%> '></asp:Label>
                                        </td >
                                          <td id="tdDistrict" runat="server">
                                             <asp:Label ID="lbldist" runat="server" Text='<%# Eval("DISTRICT")%>'></asp:Label>
                                        </td>
                                         <td id="tdSurNo" runat="server">
                                            <asp:Label ID="lblsno" runat="server" Text=' <%# Eval("SURVEYNO")%>' ></asp:Label>
                                        </td>
                                         <td id="tdECNo" runat="server"   >
                                         <%# Eval("ECNO") %>
                                       </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                

                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                        <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdnBlobCon" runat="server" />
                        <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="preview" role="dialog" style="display: none; margin-left: -100px;">
        <div class="modal-dialog text-center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content" style="width: 700px;">
                        <div class="modal-header">
                            <%--   <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            <h4 class="modal-title">Document</h4>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12">

                                <asp:Literal ID="ltEmbed" runat="server" />

                                <%--<iframe runat="server" style="width: 100; height: 100px" id="iframe2"></iframe>--%>

                                <%--<asp:Image ID="imgpreview" runat="server" Height="530px" Width="600px"  />--%>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:HiddenField ID="hdnfilename" runat="server" />
                            <asp:Button ID="BTNCLOSE" runat="server" Text="CLOSE" OnClick="BTNCLOSE_Click" OnClientClick="CloseModal();return true;" CssClass="btn btn-outline-danger" />
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript">
        function CloseModal() {
            $("#preview").modal("hide");
        }
        function ShowModal() {
            $("#preview").modal("show");
        }
    </script>

    <script>
        function validateNumberInput(input) {
            // Remove non-numeric characters using a regular expression
            input.value = input.value.replace(/[^0-9]/g, '');
        }
    </script>
</asp:Content>

