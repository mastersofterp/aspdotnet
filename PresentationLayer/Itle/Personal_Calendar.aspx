<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Personal_Calendar.aspx.cs" Inherits="Itle_Personal_Calendar" Title="" %>

<%@ Register TagPrefix="ECalendar" Namespace="ExtendedControls" Assembly="EventCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #ctl00_ContentPlaceHolder1_Calendar1 tbody th {
            text-align: center !important;
        }

        #ctl00_ContentPlaceHolder1_Calendar1 tbody td {
            padding: 6px;
        }

            #ctl00_ContentPlaceHolder1_Calendar1 tbody td a {
                display: block;
                padding-bottom: 3px;
            }

            #ctl00_ContentPlaceHolder1_Calendar1 tbody td span {
                color: White;
                background-color: #a389d4;
                padding: 3px 8px 4px;
                border-radius: 3px;
                margin: 5px;
                box-shadow: 0 2px 4px rgb(0 0 0 / 20%);
                font-weight: 500;
            }

        .TodayDate a {
            background: #7474fb;
            border-radius: 50%;
            padding: 5px;
            color: #fff !important;
            display: table-cell !important;
        }
    </style>

    <script type="text/javascript">
        $(window).on("load", function () {
            $('br').remove();
            $('span').after('<br><br>');
        });
    </script>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PERSONAL CALENDAR</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlCalendar" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Calendar</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12" id="DtEntry" runat="server">
                            <div class="row">
                                <div class="col-md-6 col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Header</label>
                                            </div>
                                            <asp:TextBox ID="txtHeader" runat="server" CssClass="form-control" TabIndex="1"
                                                ToolTip="Enter Header"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvHeader" runat="server" ControlToValidate="txtHeader"
                                                Display="None" ErrorMessage="Please Enter Header." ValidationGroup="submit" SetFocusOnError="True" />
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Description </label>
                                            </div>
                                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TabIndex="2"
                                                ToolTip="Enter Description"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDesc"
                                                Display="None" ErrorMessage="Please Enter the Description." ValidationGroup="submit" SetFocusOnError="True"
                                                InitialValue="0" />
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" TabIndex="3"
                                                OnClick="btnSubmit_Click" ToolTip="Click here to Submit"
                                                CssClass="btn btn-primary" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="4"
                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                            <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-12 table-responsive" id="listRow" runat="server">
                                    <div class="sub-heading">
                                        <h5>List of Asset Allotment</h5>
                                    </div>
                                    <asp:Panel ID="pnlAssetAllotment" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvAssetAllotment" runat="server">
                                            <EmptyDataTemplate>
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="lblEmpty" Text="No records Found" runat="server"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divsessionlist">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Delete
                                                                </th>
                                                                <th>Header
                                                                </th>
                                                                <th>Description
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                            CommandArgument='<%# Eval("ID") %>' AlternateText="Edit Record"
                                                            ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="10" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnDeleteQualDetail" runat="server" OnClick="btnDeleteQualDetail_Click"
                                                            CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("EventHeader")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EventDescription")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                    <div class="text-center d-none">
                                        <div class="vista-grid_datapager">
                                            <asp:DataPager ID="dpAessetAllotment" runat="server" PagedControlID="lvAssetAllotment"
                                                PageSize="10" OnPreRender="dpAessetAllotment_PreRender">
                                                <Fields>
                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="6" CurrentPageLabelCssClass="current" />
                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <ECalendar:EventCalendar ID="Calendar1" runat="server" BackColor="#ffffff" BorderColor="#eeeeee"
                                BorderWidth="1px" Font-Names="Verdana" Font-Bold="true" Font-Size="9pt" ForeColor="Black"
                                Height="500px" Width="100%" FirstDayOfWeek="Monday" NextMonthText="Next &gt;" HorizontalAlign="Center"
                                PrevMonthText="&lt; Prev" SelectionMode="DayWeekMonth" ShowGridLines="True" NextPrevFormat="ShortMonth"
                                ShowDescriptionAsToolTip="True" BorderStyle="Solid" EventDateColumnName="" EventDescriptionColumnName=""
                                EventHeaderColumnName="" OnSelectionChanged="Calendar1_SelectionChanged"   >
                                
                                <SelectedDayStyle BackColor="#ccff99" ForeColor="#333333" />
                                <TodayDayStyle BackColor="#ffcc99"   />
                                <SelectorStyle BackColor="#eaeaff" BorderColor="#eeeeee" BorderStyle="Solid" />
                                <DayStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="True" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <NextPrevStyle Font-Size="8pt" ForeColor="#333333" Font-Bold="True" VerticalAlign="Bottom" />
                                <%--#3C8DBC--%>
                                <DayHeaderStyle BorderWidth="1px" Font-Bold="True" Font-Size="10pt" BackColor="#eaeaff" HorizontalAlign="Center" />
                                <%--99C299--%>
                                <TitleStyle BackColor="#ccccff" BorderColor="#eeeeee" BorderWidth="1px" Font-Bold="True"
                                    Font-Size="14pt" ForeColor="#333399"   HorizontalAlign="Center" VerticalAlign="Middle"  />
                             <%--   <DayHeaderStyle BackColor="#ffffff" Font-Size="10pt" />--%>
                            </ECalendar:EventCalendar>
                        </div>

                        <div class="col-12">
                            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
                                TargetControlID="div" PopupControlID="div"
                                OkControlID="btnOkDel" OnOkScript="okDelClick();"
                                CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />

                            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                <div class="text-center">

                                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                                    <label>&nbsp;&nbsp;Are you sure you want to delete this record..?</label>
                                    <div class="text-center mt-3">
                                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-warning" />
                                    </div>

                                </div>
                            </asp:Panel>

                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>
</asp:Content>
