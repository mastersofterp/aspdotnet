<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeeLedgerHeadMapping.aspx.cs" Inherits="FeeLedgerHeadMapping" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }
    </style>
    <%-- <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    <script src="../jquery/jquery-1.10.2.js" type="text/javascript"></script>--%>

    <script language="javascript" type="text/javascript">

        function CheckNumeric(obj) {


            var k = (window.event) ? event.keyCode : event.which;

            // alert(k);

            if (k == 68 || k == 67 || k == 8 || k == 9 || k == 36 || k == 35 || k == 16 || k == 37 || k == 38 || k == 39 || k == 40 || k == 46 || k == 13 || k == 110) {
                if (obj.value == '') {
                    alert('Field Cannot Be Blank');
                    obj.focus();
                    return false;
                }
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k > 45 && k < 58 || k > 95 && k < 106) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {

                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }



        function updateValues(popupValues) {
            document.getElementById('ctl00_ContentPlaceHolder1_hdnPartyNo').value = popupValues[0];
            document.forms(0).submit();
        }

        function Checkfeestransfer(degreeno, rettype, compcode) {
            var isconfirm = '';
            $.ajax({
                type: "POST",
                url: "FeeLedgerHeadMapping.aspx/CheckAlreadyFeesTransfer",
                data: '{DegreeNo:"' + degreeno + '",RETTYPE:"' + rettype + '",compcode:"' + compcode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    isTransfer = response.d;
                    if (isTransfer == 'Available') {
                        var a = confirm("Fees is already transfered, Still want to change mapping?");
                        alert(a);
                        if (a) {
                            isconfirm = true;


                        }
                        else {
                            isconfirm = false;
                            alert(a);
                            document.getElementById('<%=hdnConfirm.ClientID %>').value = isconfirm;
                        }
                    }

                },
                failure: function (response) {
                    alert(response.d);
                }

            });
            return isconfirm
        }

        function a() {
            confirm("Fees is already transfered, Still want to change mapping?")
        }
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDLedger"
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
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">FEE-LEDGER HEAD MAPPING</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                                </div>
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12 mt-3">
                                    <%-- <div class="sub-heading">Add/Modify Fee-Ledger head Mapping</div>--%>

                                     <div class="row">
                                        <div id="Div3" class="form-group col-lg-5 col-md-6 col-12" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButton ID="rdoGenralFees" runat="server" Text="General Fees" GroupName="FeeType"
                                                AutoPostBack="true" OnCheckedChanged="rdoGenralFees_CheckedChanged" Checked="true" />
                                            <asp:RadioButton ID="rdoMiscFees" runat="server" Text="Miscellaneous Fees" GroupName="FeeType"
                                                AutoPostBack="true" OnCheckedChanged="rdoMiscFees_CheckedChanged" />

                                        </div>
                                        </div>
                                    <div class="row">
                                       
                                        <div class="col-lg-5 col-md-6 col-12" id="row18" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <span style="color:red; font-weight:bold;">*</span><label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" AppendDataBoundItems="true" 
                                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                 <div id="Div1" class="form-group col-lg-6 col-md-6 col-12" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <span style="color:red; font-weight:bold;">*</span><label>Branch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="false"
                                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                 <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                         <span style="color:red; font-weight:bold;">*</span><label>Receipt type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRecept" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True"  AppendDataBoundItems="true"  
                                                        OnSelectedIndexChanged="ddlRecept_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>

                                             
                                            </div>
                                        </div>
                                        <div class="col-lg-5 col-md-6 col-12" id="Div4" runat="server">
                                            <div class="row">
                                                  <div id="Divbatch" class="form-group col-lg-6 col-md-6 col-12" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <span style="color:red; font-weight:bold;">*</span><label>Admission Batch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlbatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="false" >
                                                       <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-6 col-md-6 col-12" id="DivSem" runat="server" visible="false">
                                                    <div  class="label-dynamic" >
                                                        <sup></sup>
                                                        <span style="color:red; font-weight:bold;">*</span><label>Semester</label>
                                                    </div>
                                                    <div id="Div7" runat="server">
                                                        <asp:DropDownList ID="ddsem" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="false">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-lg-5 col-md-6 col-12" id="row4" runat="server">
                                            <div class="row">
                                                 
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic" runat="server" visible="false">
                                                        <sup></sup>
                                                        <label>College Code</label>
                                                    </div>
                                                    <div runat="server" visible="false">
                                                        <asp:DropDownList ID="ddlAided_NoAided" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="false">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                            <asp:ListItem Value="1">E021</asp:ListItem>
                                                            <asp:ListItem Value="2">E057</asp:ListItem>
                                                            <asp:ListItem Value="3">B292</asp:ListItem>
                                                            <asp:ListItem Value="4">B292BC</asp:ListItem>
                                                            <asp:ListItem Value="5">B292BR</asp:ListItem>
                                                            <asp:ListItem Value="6">E721 </asp:ListItem>
                                                            <asp:ListItem Value="7">B292BD</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer" id="Tr1" runat="server">
                                        <asp:Button ID="btnShowData" runat="server" Text="Show" OnClick="btnShowData_Click"
                                            CssClass="btn btn-primary" />
                                        <asp:HiddenField ID="hdnConfirm" runat="server" />
                                    </div>

                                    <div class="form-group row" id="rowgrid" runat="server">
                                        <div class="col-12">
                                            <input id="hdnAgParty" runat="server" type="hidden" />
                                            <input id="hdnOparty" runat="server" type="hidden" />
                                        </div>
                                        <div class="col-12">
                                            <asp:Panel ID="ScrollPanel" runat="server">
                                                <div class="table table-responsive">
                                                    <asp:GridView ID="GridData" runat="server" CellPadding="4" GridLines="Vertical"
                                                        CssClass="table table-striped table-bordered nowrap"
                                                        AutoGenerateColumns="False"
                                                        BorderStyle="None" BorderWidth="1px" OnRowCreated="GridData_RowCreated">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="&nbsp;F.No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFeeHeadsNo" runat="server" Text='<%#Bind("FEE_HEAD") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="&nbsp;Fee Heads" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFeeHeads" runat="server" Text='<%#Bind("FEE_LONGNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="&nbsp;Ledger Head" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlleagerHead" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="&nbsp;Cash" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddllCash" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="&nbsp;Bank" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlBank" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCC99" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="#000" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <div class="row" id="Row20" runat="server">
                                        <div class="col-12">
                                            <input id="hdnAskSave" runat="server" type="hidden" />
                                            <input id="hdnVchId" runat="server" type="hidden" />
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Validation"
                                                OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Cancel"
                                                CssClass="btn btn-warning" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <input id="hdnbal2" runat="server" type="hidden" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg1" runat="server">

        <script type='text/javascript' language='javascript'>
            function askConfirm() {
                if (confirm('Fees is already transfered, Still want to change mapping?')) {
                    return true;
                }
                else {
                    return false;
                }
            }

        </script>

    </div>
</asp:Content>
