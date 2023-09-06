
//---------------------------------------------------------------
//
//
//  CheckNegative
//
//
//---------------------------------------------------------------
//
function CheckNegative(sender,Neg,Need) {
 debugger;
  var k =(window.event)? event.keyCode : event.which;

  
  
 
    if (k == 189) { // dash (-)
        if (sender.value.indexOf('-', 0) > 0)
            sender.value = sender.value.replace('-', '');
    }
}

//---------------------------------------------------------------
//
//
//  CheckInteger
//
//
//---------------------------------------------------------------
//



function CheckInteger(sender,numberOfInteger,allowNegative,Need) {
          var valueArr; 
        debugger;
          valueArr = sender.value;
           var k =(window.event)? event.keyCode : event.which;

           
 if (Need=='Yes')
  {
   if (k==9)
   {
    if (sender.value=='')
    {
      alert('Field can not be blank.');
      sender.focus();
    return false;
    }
   
   }
   
  }
    
                      
      if (k >= 48 && k <= 57 || k >= 96 && k <= 105) { // 0-9 numbers
         
            if (valueArr.indexOf('-', 0) > -1)
                numberOfInteger++;

            if (valueArr.length <= numberOfInteger-1)
            {
             
                return true;
            }
            else
            {
               return false;
            }
    
        
         
        }
        else if ((k >= 37 && k <= 40) || // Left, Up, Right and Down
        k == 8 || // backspaceASKII
        k == 9 || // tabASKII
        k == 16 || // shift
        k == 17 || // control
        k == 35 || // End
        k == 36 || // Home
        k == 46) // deleteASKII
        return true;
    else if (k == 189 && allowNegative == true) { // dash (-)
        if (sender.value.indexOf('-', 0) > -1)
            return false;
        else
            return true;
            
  
       
         }
        
        else
        {
        return false;    
           
            
        }
    
}

function CheckChar(ob,Need)
{
debugger;
 var k =(window.event)? event.keyCode : event.which;
// if (k==9)
//  {
//      ob.style.backgroundColor ='White' ;
//  
//  }
 if (Need=='Yes')
  {
   if (k==9)
   {
    if (ob.value=='')
    {
     alert('Field can not be blank.');
      ob.focus();
      return false;
    
    }
   
   }
   
  }
 
 if (k==9 || k==46)
 {
  return true;
 }
 
 if (k >= 58 && k <= 95)
      
 return true;
else 
return false;

}

function CheckVarchar(ob,Need)
{
 var k =(window.event)? event.keyCode : event.which;
// if (k==9)
//  {
//      ob.style.backgroundColor ='White' ;
//  
//  }
 if (Need=='Yes')
  {
   if (k==9)
   {
    if (ob.value=='')
    {
     alert('Field can not be blank.');
      ob.focus();
      return false;
    }
   
   }
   
  }

 return true;
}





function CheckDate(ob,Need)
{
debugger;
 var dateLength=ob.value.length;
 
  var tabkey = (window.event)? event.keyCode : event.which; //k;  //
  
   if (Need=='Yes')
  {
   if (tabkey==9)
   {
    if (ob.value=='')
    {
     alert('Field can not be blank.');
      ob.focus();
      return false;
    }
   
   }
   
  }
  
  
  
   if(tabkey==0 || tabkey==8)
    {
       return true;
    }
   if (tabkey==35 || tabkey==36 || tabkey==37 || tabkey==39 || tabkey==46 ) 
   {
       return true;
   }
      
    if (tabkey > 47 && tabkey < 58 || tabkey > 95 && tabkey < 106 || tabkey==190 || tabkey==110 || tabkey==13 || tabkey==9|| tabkey==27 )
   { 
   }
   else
   {
    return false;
   }
       


    if(dateLength < 8 && tabkey==13)
    {
   
    return false;
    }
  

if (tabkey==9)
{
    var chk=FormatDate(ob,dateLength);
            
             if(chk==false)
             {
               return false;
             }
}

               return true;
   
}



function FormatDate(field,dateLength)
{
//  if(dateLength >= 8) 
//   {
 
   
 var checkstr = "0123456789";
var DateField = field;
var Datevalue = "";
var DateTemp = "";
var seperator = "/";
var day;
var month;
var year;
var leap = 0;
var err = 0;
var i;
   err = 0;
  
   DateValue = DateField.value;
   /* Delete all chars except 0..9 */
   for (i = 0; i < DateValue.length; i++)
    {
	  if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) 
	  {
	     DateTemp = DateTemp + DateValue.substr(i,1);
	  }
   }
   DateValue = DateTemp;
   /* Always change date to 8 digits - string*/
   /* if year is entered as 2-digit / always assume 20xx */
   if (DateValue.length == 6) 
   {
      DateValue = DateValue.substr(0,4) + '20' + DateValue.substr(4,2); 
   }
   if (DateValue.length != 8)
    {
      err = 19;
    }
   /* year is wrong if year = 0000 */
     year = DateValue.substr(4,4);
   if (year == 0)
    {
       err = 20;
    }
   /* Validation of month*/
   month = DateValue.substr(2,2);
   if ((month < 1) || (month > 12)) 
   {
      err = 21;
   }
   /* Validation of day*/
   day = DateValue.substr(0,2);
   if (day < 1) 
   {
     err = 22;
   }
   /* Validation leap-year / february / day */
   if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) 
   {
      leap = 1;
   }
   if ((month == 2) && (leap == 1) && (day > 29)) 
   {
      err = 23;
   }
   if ((month == 2) && (leap != 1) && (day > 28)) 
   {
      err = 24;
   }
   /* Validation of other months */
   if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) 
   {
      err = 25;
   }
   if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) 
   {
      err = 26;
   }
   /* if 00 ist entered, no error, deleting the entry */
   if ((day == 0) && (month == 0) && (year == 00)) 
   {
      err = 0; day = ""; month = ""; year = ""; seperator = "";
   }
   /* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
   if (err == 0) 
   {
      DateField.value = day + seperator + month + seperator + year;
   }
   /* Error-message if err != 0 */

    
else 
    {
  
      alert('Enter Date in dd/mm/yyyy format!');
      field.focus();
      return false;
        

   }



}


 




//---------------------------------------------------------------
//
//
//  CheckDecimal
//
//
//---------------------------------------------------------------
//
function CheckDecimal(sender, numberOfInteger, numberOfFrac, allowNegative,Need) {
debugger;
    var valueArr;
  var k =(window.event)? event.keyCode : event.which;

if (k==9)
  {
      sender.style.backgroundColor ='White' ;
  
  }


 if (Need=='Yes')
  {
   if (k==9)
   {
    if (sender.value=='')
    {
     alert('Field can not be blank.');
      sender.focus();
      return false;
    }
   
   }
   
  }

    if ((k >= 37 && k <= 40) || // Left, Up, Right and Down
    	k == 8 || // backspaceASKII
        k == 9 || // tabASKII
        k == 16 || // shift
        k == 17 || // control
        k == 35 || // End
        k == 36 || // Home
        k == 46) // deleteASKII
        return true;
    else if (k == 189 && allowNegative == true) { // dash (-)
        if (sender.value.indexOf('-', 0) > -1)
            return false;
        else
            return true;
    }

    valueArr = sender.value.split('.');

    

    if (k == 190 ||  k ==110 ) { // decimal point (.)
        if (valueArr[0] != null && valueArr[1] == null)
            return true;
        else
            return false;
    }

    if (k >= 48 && k <= 57 || k >= 96 && k <= 105) { // 0-9 numbers
        if (valueArr[1] == null) {
            if (valueArr[0].indexOf('-', 0) > -1)
                numberOfInteger++;

            if (valueArr[0].length <= numberOfInteger-1)
                return true;
        }
        else {
            if (valueArr[1].length <= numberOfFrac-1)
                return true;
        }
    }

    return false;
}



//Checking PAN Number


 function CheckPAN(val,numtype,obj,Need)
 {
 debugger;
if (val=='pan')
{
 if (numtype=='Zero')
 {
   var k=(window.event)? event.keyCode : event.which;
   
   
   if (k==9)
  {
      obj.style.backgroundColor ='White' ;
     return true;
  }
   
   
   if (Need=='Yes')
  {
   if (k==9)
   {
    if (obj.value=='')
    {
     alert('Field can not be blank.');
      obj.focus();
      return false;
    }
   
   }
   
  }
  
   //var k=(event.which)? event.which : event.keyCode;
   var pan=obj.value;
   var plength=pan.length;
     
      if (k==8 || k==0 || k==35 || k==36 || k==37 || k==39 || k==46)
     {
       return true ;
     }
    
      
      if (k < 91 && k > 64)
     {
      if (plength < 5)
      {
      
       return true;
      }
      else
      {
        if (plength==9)
        {
        lastChar(k);
          if(lastChar(k)==false)
          {
          return false;
          }
          else
          {
            
          return true;
          }
        }
      }
   }   
  else
  {
  //added========
  if (k > 46 && k < 58 ) 
       {
       
       }
       else
       {
        return false;
       }
  //=============
  
  
  
  if(plength < 5)
  {
  alert('Enter First Five Charecter Only!.');
  obj.value="";
   
  }
  }
 
if (k > 46 && k < 58 )
       
 {
  var nlength=pan.length;
   if (nlength > 4 && nlength < 9)
   {
     
    return true;    
   }
   else
   {
   return false;
   } 
     
 }
    //======================
else
 {
  return false;
 }      
 }    
 else
 {
 return false;
 
 } 
   
} 
}
function getFocus(obj)
{      
debugger; 
   var s=obj.id;
 obj.style.backgroundColor =SetTextBackColor(); 
   var objRange = obj.createTextRange();
   objRange.moveStart("character", 0);
   objRange.moveEnd("character", obj.value.length);
   objRange.select();
   obj.focus();
   return false;
}

function lostFocus(obj)
{    
debugger;   
   var s=obj.id;
   obj.style.backgroundColor ='White' ;


}



/* textbox highlighting*/
// creates a function to set the css class name of input controls when they
// receive or lose focus.
/*
setAspnetTextFocus = function() {
    // CSS class name to use when no focus is on the input control
    var classBlur = 'input_text';
    // CSS class name to use when the input control has focus
    var classFocus = 'input_text_focus';
    // get all of the input tags on the page
    var inputElements = document.getElementsByTagName('input');

    for (var i = 0; i < inputElements.length; i++) {
        // add the onfocus event and set it to add the on focused CSS class
        inputElements[i].onfocus = function() {
            if (this.className == 'input_text') {
                this.className += ' ' + classFocus;
            }
        }
        // add the onblur event and set it to remove the on focused CSS class when it loses focus
        inputElements[i].onblur = function() {
            this.className = this.className.replace(new RegExp(' ' + classFocus + '\\b'), '');
        }
    }
}
// attach this event on load of the page
if (window.attachEvent) window.attachEvent('onload', setAspnetTextFocus);

*/