/*  Previously these functions were used in Fees Collection functionality. 
    Currently these functions have not been used any where. */

var xmlHttp = null;

function SearchByName(searchStr)
{
    try
    {
        if(searchStr.length < 3)return;
        
        xmlHttp = GetXmlHttpObject();
        if (xmlHttp == null) return;
        
        xmlHttp.onreadystatechange = BindListBox;

        var url = document.location.toString();
        url = url.substr(0, (url.indexOf('FeeCollection.aspx', 0)));
        url += "SearchStudent.aspx?searchStr=" + searchStr + "&fieldName=studname";
        xmlHttp.open("GET", url, true);
        xmlHttp.send(null);
        return;
    }
    catch(e)
    {
        alert("Error: " + e.description);
    }
}

function SearchByEnrollmentNo(searchStr)
{
    try
    {    
        if(searchStr.length < 3)return;
        
        xmlHttp = GetXmlHttpObject();
        if (xmlHttp == null) return;
        
        xmlHttp.onreadystatechange = BindListBox;
        
        var url = document.location.toString();
        url = url.substr(0, (url.indexOf('FeeCollection.aspx', 0)));
        url += "SearchStudent.aspx?searchStr=" + searchStr + "&fieldName=regno";
        xmlHttp.open("GET", url, true);
        xmlHttp.send(null);
        return;
    }
    catch(e)
    {
        alert("Error: " + e.description);
    }
}

function BindListBox()
{
    try
    {
        if(xmlHttp.readyState == 4)
        {
            var response  = xmlHttp.responseText;        
            response = response.substr(0, response.indexOf("<!DOCTYPE", 0));
            document.getElementById('ctl00_ContentPlaceHolder1_lstStudentList').innerHTML = response;        
            return;
        }
    }
    catch(e)
    {
        alert("Error: " + e.description);
    }
}

function GetXmlHttpObject()
{
    var xmlHttpObj = null;
    try
    {
      // Firefox, Opera 8.0+, Safari
      xmlHttpObj = new XMLHttpRequest();
    }
    catch (e)
    {
        // Internet Explorer
        try
        {
            xmlHttpObj = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e)
        {
            try
            {
                xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e)
            {
                alert("Your browser does not support AJAX!");
                return false;
            }
        }
    }
    return xmlHttpObj;
}

function GetMachingListboxItem(strTxt)
{ 
    var listbox = document.getElementById("ctl00_ContentPlaceHolder1_lstStudentList");
    
    if (strTxt != "")
    {   
        for(var index = 0; index <= listbox.length; index++)
        {
            var  strLst = listbox.options[index].innerHTML;            
            if(strLst.toUpperCase().substring(0,strTxt.length) == strTxt.toUpperCase())
            {
               listbox.selectedIndex = index;
               return;
            }
            else
            {
                listbox.selectedIndex = -1;
            }
        }
    }
    return false;
}
