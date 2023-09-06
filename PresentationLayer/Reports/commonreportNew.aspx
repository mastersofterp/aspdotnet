<%@ Page Language="C#" AutoEventWireup="true" CodeFile="commonreportNew.aspx.cs" Inherits="Reports_commonreportNew" %>


<%@ Register Assembly ="CrystalDecisions.Web,Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function printArea(areaName) {
            var printContents = document.getElementsByClassName(areaName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }


        var gAutoPrint = true; // Flag for whether or not to automatically call the print function 
        function printSpecial() {
            if (document.getElementById != null) {
                var html = '<HTML>\n<HEAD>\n';
                if (document.getElementsByTagName != null) {
                    var headTags = document.getElementsByTagName("head");
                    if (headTags.length > 0) html += headTags[0].innerHTML;
                }
                if (gAutoPrint) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        html += '\n</HEAD>\n<'
                        html += 'BODY onLoad="PrintCommandObject.ExecWB(6, -1);">\n';
                    }
                    else {
                        html += '\n</HEAD>\n<BODY>\n';
                    }
                }
                else {
                    html += '\n</HEAD>\n<BODY>\n';
                }
                debugger;
                var printReadyElem = document.getElementById("crViewer");
                if (printReadyElem != null) {
                    html += printReadyElem.innerHTML;
                }
                else {
                    alert("Could not find the printReady section in the HTML");
                    return;
                }
                if (gAutoPrint) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        html += '<OBJECT ID="PrintCommandObject" WIDTH=0 HEIGHT=0 '
                        html += 'CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>\n</BODY>\n</HTML>';
                    }
                    else {
                        html += '\n</BODY>\n</HTML>';
                    }
                }
                else {
                    html += '\n</BODY>\n</HTML>';
                }
                var printWin = window.open("", "printSpecial");
                printWin.document.open();
                printWin.document.write(html);
                printWin.document.close();
                if (gAutoPrint) {
                    if (navigator.appName != "Microsoft Internet Explorer") {
                        printWin.print();
                    }
                }
            }
            else {
                alert("Sorry, the print ready feature is only available in modern browsers.");
            }
        }



        </script>



  <script type="text/javascript">
      function Print() {
          debugger;
          var dvReport = document.getElementById("dvReport");
          var frame1 = dvReport.getElementsByTagName("iframe")[0];
          if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1) {
              frame1.name = frame1.id;
              window.frames[frame1.id].focus();
              window.frames[frame1.id].print();
          }
          else {
              var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
              frameDoc.print();
          }
      }
    </script>
<%-- HasExportButton ="True" 
            HasDrilldownTabs ="False" 
            HasCrystalLogo="False" 
            HasGotoPageButton ="True"
            HasPageNavigationButtons ="True" 
            HasRefreshButton ="True" 
            HasToggleGroupTreeButton="false" 
            HasPrintButton ="True" 
            HasZoomFactorList ="True" 
            HasSearchButton ="True"
            HasToggleParameterPanelButton ="true"--%>


</head>
<body>
    <form id="form1" runat="server">
    <div id="dvReport">
        <CR:CrystalReportViewer ID="crViewer" runat="server" 
            AutoDataBind="true"
           BestFitPage="False" ToolPanelView="None" OnUnload="crViewer_Unload"/>
      
    </div>
    </form>
</body>
</html>
