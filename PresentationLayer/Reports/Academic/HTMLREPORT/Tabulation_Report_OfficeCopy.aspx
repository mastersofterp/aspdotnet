<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Tabulation_Report_OfficeCopy.aspx.cs" Inherits="Reports_Tabulation_Report_OfficeCopy" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link rel="shortcut icon" href="IMAGES/logo.png" type="image/x-icon">

    <link href="plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <script src="plugins/newbootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="plugins/newbootstrap/js/popper.min.js"></script>
    <script src="plugins/newbootstrap/js/bootstrap.min.js"></script>--%>
    <script src="path/to/pdfjs-dist/build/pdf.js"></script>

    <style>
        @font-face {
            font-family: 'opensans';
            src: url('plugins/newbootstrap/webfont/opensans/regular/opensans-regular-webfont.eot');
            src: url('plugins/newbootstrap/webfont/OpenSans-Regular-webfont.eot?iefix') format('eot'), url('plugins/newbootstrap/webfont/OpenSans-Regular-webfont.woff') format('woff'), url('plugins/newbootstrap/webfont/OpenSans-Regular-webfont.ttf') format('truetype'), url('plugins/newbootstrap/webfont/OpenSans-Regular-webfont.svg') format('svg');
        }

        body {
            font-family: 'opensans', sans-serif;
            font-size: 12px;
        }

        label {
            font-weight: 600;
        }

        .main-details {
            border-top: 1px solid #333;
        }

        .card-detail {
            text-decoration: underline;
        }

        .report-div {
            font-family: Verdana, sans-serif !important;
        }
        /*#ContainerPdf {
            page-break-inside: auto;
        }*/
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Tabulation Report Office Copy</h3>
                    <div class="text-right mll-auto">
                        <%--<button type="button" class="btn btn-secondary position-relative" style="z-index: 888; padding: 0.25rem 0.7rem;" onclick="loadAndPrintPDF()">--%>
                        <button type="button" class="btn btn-secondary position-relative" style="z-index: 888; padding: 0.25rem 0.7rem;" onclick="printPDF()">
                            <i class="fa fa-download"></i>
                        </button>
                        <%--  <asp:Button runat="server" ID="btnprint"  OnClick="btnprint_Click"/>--%>
                    </div>
                </div>
                <div class="box-body">
                   <%-- <div class="text-dark">
                        <div class="col-12 text-center">
                            <asp:Image ID="imglogo" runat="server" ImageUrl="~/Images/Login/Hindustan_logo.png" AlternateText="logo" /><br />
                            <label>B.Tech- I Semester- Regular- University Exam (2022 Regulation)- Nov 2022</label><br />
                            <label>B.Tech BIO TECHNOLOGY SEMESTER 1 - 2022 Regulation</label><br />
                            <label>SEMESTER EXAMINATION RESULT</label>
                        </div>
                        <div class="col-12 mt-3">
                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                <thead class="bg-light-blue">
                                    <tr>
                                        <th>Reg No</th>
                                        <th>Name</th>
                                        <th class="text-center">C 1
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">                                            
                                            <span>CIA</span>
                                            <span>ES</span>
                                            <span>T</span>
                                            <span>G</span>
                                            <span>/50</span>
                                            <span>E</span>
                                        </div>
                                        </th>
                                        <th class="text-center">C 2
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>Gr</span>
                                            <span>CIA/50</span>
                                        </div>
                                        </th>
                                        <th class="text-center">C 3
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>Gr</span>
                                            <span>CIA/50</span>
                                        </div>
                                        </th>
                                        <th class="text-center">C 4
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>Gr</span>
                                            <span>CIA/50</span>
                                        </div>
                                        </th>
                                        <th class="text-center">C 5
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>Gr</span>
                                            <span>CIA/50</span>
                                        </div>
                                        </th>
                                        <th class="text-center">C 6
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>Gr</span>
                                            <span>CIA/50</span>
                                        </div>
                                        </th>
                                        <th class="text-center">C 7
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>Gr</span>
                                            <span>CIA/50</span>
                                        </div>
                                        </th>
                                        <th class="text-center">C 8
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>Gr</span>
                                            <span>CIA/50</span>
                                        </div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>22132001</td>
                                        <td>PUSHPA V</td>
                                        <td class="text-center">EMA51001
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>38</span>
                                            <span>32</span>
                                            <span>70</span>
                                            <span>A</span>
                                        </div>
                                        </td>
                                        <td class="text-center">EMA51001
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>A</span>
                                            <span>38</span>
                                        </div>
                                        </td>
                                        <td class="text-center">EMA51001
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>A</span>
                                            <span>38</span>
                                        </div>
                                        </td>
                                        <td class="text-center">EMA51001
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>A</span>
                                            <span>38</span>
                                        </div>
                                        </td>
                                        <td class="text-center">EMA51001
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>A</span>
                                            <span>38</span>
                                        </div>
                                        </td>
                                        <td class="text-center">EMA51001
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>A</span>
                                            <span>38</span>
                                        </div>
                                        </td>
                                        <td class="text-center">EMA51001
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>A</span>
                                            <span>38</span>
                                        </div>
                                        </td>
                                        <td class="text-center">EMA51001
                                        <div class="d-flex justify-content-between align-items-center" style="font-size: 10px;">
                                            <span>A</span>
                                            <span>38</span>
                                        </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="col-12">
                            <p style="font-size: 9px;"><span>CIA - Continuous Internal Assessment; G- Grade; Marks: 90 - 100 Grade 'S'; 80 - 89 Grade 'A+'; 70 - 79 Grade 'A'; 60 - 69 Grade 'B+'; 55 - 59 Grade 'B'; 50 - 54 Grade 'C';45 - 49 Grade 'P'; 0 - 44 Grade 'F'; AB – Absent; RA – Repeat</span></p>
                        </div>
                        <div class="col-12 mt-3">
                            <div class="border-bottom">
                                <div class="row pb-2">
                                    <div class="col-6 text-center">
                                        <label><b>Controller of Examinations</b></label>
                                    </div>
                                    <div class="col-6 text-center">
                                        <label><b>Vice Chancellor</b></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mt-5">
                            <div class="row">
                                <div class="col-12 col-md-4">
                                    <p class="mb-1">EMA51001 MATRICES AND CALCULUS (SM/50);</p>
                                    <p class="mb-1">EMA51001 MATRICES AND CALCULUS (SM/50);</p>
                                    <p class="mb-1">EMA51001 MATRICES AND CALCULUS (SM/50);</p>
                                </div>
                                <div class="col-12 col-md-4">
                                    <p class="mb-1">EMA51001 MATRICES AND CALCULUS (SM/50);</p>
                                    <p class="mb-1">EMA51001 MATRICES AND CALCULUS (SM/50);</p>
                                    <p class="mb-1">EMA51001 MATRICES AND CALCULUS (SM/50);</p>
                                </div>
                                <div class="col-12 col-md-4">
                                    <p class="mb-1">EMA51001 MATRICES AND CALCULUS (SM/50);</p>
                                    <p class="mb-1">EMA51001 MATRICES AND CALCULUS (SM/50);</p>
                                    <p class="mb-1">EMA51001 MATRICES AND CALCULUS (SM/50);</p>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <div class="col-12" id="element-to-print">
                        <div class="row" runat="server" id="DivReport">
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnDivId" />
                    <asp:HiddenField runat="server" ID="hdndotalcount" />
                </div>
            </div>
        </div>
    </div>
    <script src="es6-promise.auto.min.js"></script>
    <%--<script src="jspdf.min.js"></script>
    <script src="html2canvas.min.js"></script>
    <script src="html2pdf.min.js"></script>--%>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%-- <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.min.js"></script>--%>
    <%--    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.8.0/html2pdf.bundle.min.js" integrity="sha512-w3u9q/DeneCSwUDjhiMNibTRh/1i/gScBVp2imNVAMCt6cUHIw6xzhzcPFIaL3Q1EbI2l+nu17q2aLJJLo4ZYg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>


        <script>
        function printPDF() {
            //document.getElementById("myFooter").style.pageBreakBefore = "always";
            var element = document.getElementById('ctl00_ContentPlaceHolder1_DivReport');

        const lastFooterP = element.querySelector('.html2pdf__page-break:last-of-type');
            if (lastFooterP) {
                lastFooterP.remove();
            }

            var opt = {
                margin: [20, 30, 20, 20],
                filename: 'TabulationReport_OfficeCopy.pdf',
                image: { type: 'jpeg', quality: 0.5 },
                html2canvas: { scale: 1.5, scrollY: 0 },
                jsPDF: { unit: 'mm', format: 'a4', orientation: 'landscape' }
                //jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
            }
            html2pdf(element, opt);

        }
</script>
    <%--<script>
        //function printPDF() {
        //    document.getElementById("myFooter").style.pageBreakBefore = "always";
        //    var element = document.getElementById('ctl00_ContentPlaceHolder1_DivReport');
        //    var opt = {

        //        margin:       1,
        //        filename:     'StudentReceipt.pdf',
        //        image:        { type: 'jpeg', quality: 0.98 },
        //        html2canvas:  { scale: 1, scrollY: 0 },
        //        jsPDF:        { unit: 'in', format: 'letter', orientation: 'portrait' }
        //    }
        //    html2pdf(element, opt);

        //}

        function printPDF() {

            document.getElementById("myFooter").style.pageBreakBefore = "always";
            var element = document.getElementById('ctl00_ContentPlaceHolder1_DivReport');
            var opt = {

                margin: [0, 30, 0, 20],
                filename: 'StudentReceipt.pdf',
                image: { type: 'jpeg', quality: 0.5 },
                html2canvas: { scale: 1.5, scrollY: 0 },
                jsPDF: { unit: 'mm', format: 'a4', orientation: 'landscape' }

            }

            html2pdf(element, opt);


        }

    </script>--%>


    <%-- <script>
        function printPDF() {
            //document.getElementById("myFooter").style.pageBreakBefore = "always";
            var element = document.getElementById('ctl00_ContentPlaceHolder1_DivReport');

        const lastFooterP = element.querySelector('.html2pdf__page-break:last-of-type');
            if (lastFooterP) {
                lastFooterP.remove();
            }

            var opt = {
                margin: [0, 30, 0, 20],
                filename: 'StudentReceipt.pdf',
                image: { type: 'jpeg', quality: 0.5 },
                html2canvas: { scale: 1.5, scrollY: 0 },
                jsPDF: { unit: 'mm', format: 'a4', orientation: 'landscape' }
                //jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
            }
            html2pdf(element, opt);

        }
</script>--%>
 
<%--<script>
    debugger
    document.getElementById('downloadButton').addEventListener('click', function () {
        // Get the HTML content of the specified element
        var contentToDownload = document.getElementById('DivReport');

        // Use html2pdf to generate a PDF from the HTML content
        html2pdf(contentToDownload, {
            margin: 10,
            filename: 'downloaded_content.pdf',
            image: { type: 'jpeg', quality: 0.98 },
            html2canvas: { scale: 2 },
            jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
        });
    });
</script>--%>


    <script>
        function loadAndPrintPDF() {
            // Path to your PDF file
            var pdfPath = 'path/to/your/pdf.pdf';

            // Asynchronously download PDF
            var loadingTask = pdfjsLib.getDocument(pdfPath);
            loadingTask.promise.then(pdf => {
                // Fetch the first page
                pdf.getPage(1).then(page => {
                    // Set the desired scale (1 = original size)
                    var scale = 1.0;
                    var viewport = page.getViewport({ scale: scale });

            // Prepare canvas using PDF page dimensions
                    var canvas = document.createElement('canvas');
                    var context = canvas.getContext('2d');
            canvas.height = viewport.height;
            canvas.width = viewport.width;

            // Render PDF page into canvas context
                    var renderContext = {
                        canvasContext: context,
                        viewport: viewport
                    };
                    var renderTask = page.render(renderContext);
            renderTask.promise.then(() => {
                // Print the rendered canvas
                window.print();
        });
        });
        });
        }
    </script>

    
       <script>
           function printDiv(divId) {
               var printContents = document.getElementById(divId).innerHTML;
               var originalContents = document.body.innerHTML;

               // Create a hidden iframe
               var iframe = document.createElement('iframe');
               iframe.style.height = '0';
               iframe.style.width = '0';
               document.body.appendChild(iframe);

               var iframeDoc = iframe.contentWindow.document;

               // Write the content to the iframe
               iframeDoc.write('<html><head><title>Print</title>');
               iframeDoc.write('<style>');
               iframeDoc.write('@media print {');
               iframeDoc.write('.myFooter { page-break-after: always; }'); // Page break before element with class header
               iframeDoc.write('}');
               iframeDoc.write('</style></head><body>' + printContents + '</body></html>');
               iframeDoc.close();

               // Print the iframe contents
               iframe.contentWindow.print();

               // Remove the iframe
               document.body.removeChild(iframe);

               // Restore original content
               document.body.innerHTML = originalContents;
           }
       </script>

</asp:Content>

