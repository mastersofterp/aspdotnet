<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Staff_type.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_Staff_type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STAFF TYPE MANAGEMENT</h3>
                        </div>
                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <asp:Panel ID="pnlPfMaster" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Staff Type Management</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type :</label>
                                            </div>
                                            <asp:TextBox ID="txtStaffType" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"
                                                TabIndex="1" MaxLength="150" ToolTip="Please Enter Staff Type ">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtStaffType" runat="server" ErrorMessage="Enter Staff Type " ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Active Status :</label>
                                            </div>
                                           <asp:CheckBox ID="chkActive" Checked="false"  runat="server" AutoPostBack="true"  />
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="chkActive" runat="server" ErrorMessage="check Active Status " ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>--%>
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Is Teaching:</label>
                                            </div>
                                           <asp:CheckBox ID="chkIsTeaching" Checked="false"  runat="server" AutoPostBack="true" />
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="chkIsTeaching" runat="server" ErrorMessage="check Is Teaching " ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>--%>
                                        </div>

                                    
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" TabIndex="3" Text="Submit" OnClick="btnsubmit_Click" ValidationGroup="submit"
                                    ToolTip="Submit" CssClass="btn btn-primary" />
                               
                                <asp:Button ID="btncancel" runat="server" TabIndex="5" Text="Cancel" OnClick="btncancel_Click"  CausesValidation="False"
                                    ToolTip="Cancel" CssClass="btn btn-warning" />  
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvstaff" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Staff Type List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>ID
                                                        </th>
                                                        <th>Staff Name
                                                        </th>
                                                        <th>Is Active 
                                                        </th>
                                                       <th style="display:none">Is Teaching 
                                                        </th>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("STNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblstaffno" runat="server" Text='<%# Eval("STNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblstaffname" runat="server" Text='<%# Eval("STAFFTYPE") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblisactive" runat="server" Text='<%# Eval("ACTIVESTATUS") %>'  />
                                                </td>
                                              <td style="display:none">
                                                    <asp:Label ID="lblisteaching" runat="server" Text='<%# Eval("IsTeaching") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                                <div class="vista-grid_datapager d-none">
                                    <div class="text-center">
                                        <asp:DataPager ID="DataPager1" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvstaff"
                                            PageSize="100">
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
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
                </div>
            </div>
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
 
  
</asp:Content>


