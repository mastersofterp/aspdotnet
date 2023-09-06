<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MailTemplateDataCategory.aspx.cs"
    Inherits="CreateTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .MyTopMar {
            margin-top:10px;
        }

        .MyLRMar {
            margin-left:5px;
            margin-right:5px;
        }
       
        /* Absolute Center Spinner */
        .loadingg {
            position: fixed;
            z-index: 9999;
            height: 2em;
            width: 2em;
            overflow: visible;
            margin: auto;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
        }
        /* Transparent Overlay */
        .loadingg:before {
            content: '';
            display: block;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0,0.3);
        }
    </style>

    <div style="z-index: 1; position: relative;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTemplate"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="position:fixed;margin-left:40%;margin-top:18%">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px;"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updTemplate" runat="server">
        <ContentTemplate>

            <div class="row">
             <%--  <div class="loadingg hidden">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px;"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>--%>
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Template Data Type Master</h3>
                        </div>

                        <div class="box-body">
                             <div class="col-md-4">
                                <label><span style="color: red;">* </span>Category :</label>
                                 <asp:DropDownList ID="ddlCategoty" runat="server" ClientIDMode="Static" CssClass="form-control" AppendDataBoundItems="true">
                                     <asp:ListItem Value="0">Please Select</asp:ListItem>
                                 </asp:DropDownList>
                            </div> 

                            <div class="col-md-4">
                                <label><span style="color: red;">* </span>Data Type Name :</label>
                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" placeholder="Enter Category Name" ClientIDMode="Static"></asp:TextBox>
                            </div> 
                            <div class="col-md-4">
                                <label><span style="color: red;">* </span>Stored Procedure :</label>
                                <asp:TextBox ID="txtSPName" runat="server" CssClass="form-control" placeholder="Enter SP Name" ClientIDMode="Static"></asp:TextBox>
                            </div> 
                        </div>
                        
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="85px" OnClick="btnSubmit_Click" ClientIDMode="Static" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" Width="85px" OnClick="btnCancel_Click" />
                            </p>
                            <div class="col-md-12">
                                    <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto">
                                        <div class="titlebar">
                                            <h3>
                                                <span class="label label-default pull-left">Data Type List</span> 
                                            </h3>
                                        </div>
                                        <table class="table table-hover table-bordered">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th width="7%"><center>Sr. No.</center></th>
                                                    <th width="7%"><center>Edit</center></th>
                                                    <th width="7%"><center>Delete</center></th>
                                                    <th>Name</th>
                                                    <th>Category</th>
                                                    <th>SP Name</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTemplate" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><center><%#Container.ItemIndex+1 %></center></td>
                                                            <td>
                                                                <%--<center>--%>
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Delete" CommandArgument='<%# Eval("ID")+"$"+Eval("NAME")+"$"+Eval("SP_NAME")+"$"+Eval("CATEGORY_ID") %>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" ToolTip="Edit" />
                                                                </center>
                                                            </td>
                                                            <td>
                                                                <center>
                                                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/images/delete.gif" OnClick="btnDelete_Click" OnClientClick="return ConfirmSubmit();" ToolTip="Delete" CssClass="btnDelX" />
                                                                </center>
                                                            </td>
                                                            <td><%# Eval("NAME")%></td>
                                                            <td><%# Eval("CATEGORY_NAME")%></td>
                                                            <td><%# Eval("SP_NAME")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                            </table>
                                    </asp:Panel>
                                </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="div1" runat="Server">
    </div>
    <div id="divMsg" runat="Server">
    </div>

    <script>
       
        //$(document).ready(function () {
        //   ShowLoader();
        //});

        $('.btnDelX').click(function () {
            var r = confirm("Are you sure to Delete this Record.");
            if (r == true) {
                return true;
            } else {
                return false;
            }
        });

        //function ShowLoader() {
        //    debugger;
        //    $('.loadingg').removeClass('hidden');
        //    return true;
        //}
        //function HideLoader() {
        //    debugger
        //    $('.loadingg').addClass('hidden');
        //    return true;
        //}
       
    </script>
   
</asp:Content>
