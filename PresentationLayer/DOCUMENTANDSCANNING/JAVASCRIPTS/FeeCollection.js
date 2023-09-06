
function ValidatePayType(txtPayType)
{
    try
    {
        if(txtPayType != null && txtPayType.value != '')
        {
            if(txtPayType.value.toUpperCase() == 'D')
            {
                txtPayType.value = "D";
                if(document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null)
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "block";
                    document.getElementById('ctl00_ContentPlaceHolder1_txtDDNo').focus();
                }
            }
            else
            {       
                if(txtPayType.value.toUpperCase() == 'C')
                {
                    txtPayType.value = "C";
                    if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null && 
                        document.getElementById('ctl00_ContentPlaceHolder1_divFeeItems') != null)
                    {
                        document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
                        document.getElementById('ctl00_ContentPlaceHolder1_divFeeItems').style.display = "block";
                    }
                }
                else
                {
                    alert("Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts.");            
                    if(document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
                        
                    txtPayType.value = "";
                    txtPayType.focus();
                }
            }
        }
    }
    catch(e)
    {
        alert("Error: " + e.description);
    }
}

function DevideTotalAmount()
{
    try
    {
        var totalAmt = 0;
        if( document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null && 
            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
            totalAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim());
                    
        var dataRows = null;
        if(document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
            dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');
        
        if(dataRows != null)
        {
            for(i = 1; i < dataRows.length; i++)
            {
                var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                var dataCell = dataCellCollection.item(2);
                var controls = dataCell.getElementsByTagName('input');
                var originalAmt = controls.item(1).value;
                if(originalAmt.trim() != '')
                    originalAmt = parseFloat(originalAmt);
                
                if ((totalAmt - originalAmt) >= originalAmt)
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl'+(i-1)+'_txtFeeItemAmount').value = originalAmt;
                    totalAmt = (totalAmt - originalAmt);
                }
                else
                {
                    if((totalAmt - originalAmt) >= 0)
                    {
                        document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl'+(i-1)+'_txtFeeItemAmount').value = originalAmt;
                        totalAmt = (totalAmt - originalAmt);
                    }
                    else
                    {
                        document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl'+(i-1)+'_txtFeeItemAmount').value = totalAmt;
                        totalAmt = 0;
                    }
                }
            }
        }
        UpdateTotalAndBalance();
    }
    catch(e)
    {
        alert("Error: " + e.description);
    }
}

function UpdateTotalAndBalance()
{
    try
    {
        var totalFeeAmt = 0.00;
        var dataRows = null;
        
        if(document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
             dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');
        
        if(dataRows != null)
        {        
            for(i = 1; i < dataRows.length; i++)
            {        
                var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                var dataCell = dataCellCollection.item(2);
                var controls = dataCell.getElementsByTagName('input');
                var txtAmt = controls.item(0).value;
                if(txtAmt.trim() != '')
                    totalFeeAmt += parseFloat(txtAmt);
            }
            if(document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount') != null)
                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount').value = totalFeeAmt;
                
            var totalPaidAmt = 0;
            if(document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                totalPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();

            if(document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance') != null)
                document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance').value = (parseFloat(totalPaidAmt) - parseFloat(totalFeeAmt));
        }
        UpdateCash_DD_Amount();
    }
    catch(e)
    {
        alert("Error: " + e.description);
    }    
}

function UpdateCash_DD_Amount()
{
    try
    {
        var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_txtPayType');
        var txtPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount');
        
        if(txtPayType != null && txtPaidAmt != null)
        {
            if(txtPayType.value.trim() == "C" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null)
            {            
                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
            }
            else if(txtPayType.value.trim() == "D" && document.getElementById('tblDD_Details') != null)
            {
                var totalDDAmt = 0.00;
                var dataRows = document.getElementById('tblDD_Details').getElementsByTagName('tr');    
                if(dataRows != null)
                {
                    for(i = 1; i < dataRows.length; i++)
                    {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(6);
                        if(dataCell != null)
                        {
                            var txtAmt = dataCell.innerHTML.trim();
                            totalDDAmt += parseFloat(txtAmt);
                        }
                    }
                    if( document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount') != null &&
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null)
                    {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = totalDDAmt;
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                    }
                }
            }
        }
    }
    catch(e)
    {
        alert("Error: " + e.description);
    }
}


function ShowHideDivPaidReceipts(btnControl, divId)
{
    if(btnControl != null && divId != '')
    {
        if(document.getElementById(divId) != null && document.getElementById(divId).style.display == "none")
        { 
            document.getElementById(divId).style.display = "block";
            btnControl.value = "Hide Paid Receipts Information";
        }
        else if(document.getElementById(divId) != null && document.getElementById(divId).style.display == "block")
        { 
            document.getElementById(divId).style.display = "none";
            btnControl.value = "Show Paid Receipts Information";
        }
    }
}

function IsNumeric(textbox)
{
    if(textbox != null && textbox.value != "")
    {
        if (isNaN(textbox.value))
        {
            document.getElementById(textbox.id).value = '';
        }
    }
}

function ValidateNumeric(txt)
{
    if(isNaN(txt.value))
    {
        txt.value='';       
        alert('Only Numeric Characters are Allowed.');
        txt.focus();
        return;            
    }        
}