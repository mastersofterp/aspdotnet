<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="QuotationEntrybyVendor.aspx.cs" Inherits="STORES_Vendor_QuotationEntrybyVendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #quottbl {
            display: none;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Quotation Entry by Vendor</h3>
                </div>
                <div class="box-body">
                    <div class="col-12 mt-3">
                        <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Quotation No.</th>
                                    <th>Quotation Date</th>
                                    <th>Last Submission Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><span class="quotno text-info" onclick="showContent()" style="cursor: pointer;">Main Store/Quotref67</span></td>
                                    <td>11-02-2024</td>
                                    <td>14-02-2024</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <div class="col-12 mt-4" id="quottbl">
                        <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Item Name</th>
                                    <th>Quantity</th>
                                    <th>Rate</th>
                                    <th>Disc. %</th>
                                    <th>Disc. Amount</th>
                                    <th>Tax %</th>
                                    <th>Taxable Amount</th>
                                    <th>Tax Amount</th>
                                    <th>Total Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Cupboard</td>
                                    <td>2</td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="5000.00" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="10.00" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="500.00" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="10.00" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="500.00" disabled="disabled" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="5000.00" disabled="disabled" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="5000.00" disabled="disabled" /></td>
                                </tr>
                                <tr>
                                    <td>Table</td>
                                    <td>2</td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="1000.00" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="10.00" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="100.00" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="10.00" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="100.00" disabled="disabled" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="1000.00" disabled="disabled" /></td>
                                    <td>
                                        <input type="text" class="form-control" placeholder="1000.00" disabled="disabled" /></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:button id="btnSubmit" runat="server" text="Submit" tabindex="1" cssclass="btn btn-primary" />
                        <asp:button id="btnCancel" runat="server" text="Cancel" tabindex="1" cssclass="btn btn-warning" />
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script>
        function showContent() {
            var content = document.getElementById("quottbl");
            if (content.style.display === "none") {
                content.style.display = "block"; // Show the content
            } else {
                content.style.display = "none"; // Hide the content
            }
        }
    </script>
</asp:Content>

