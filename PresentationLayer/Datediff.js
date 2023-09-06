function parseDate(str) { 
    var date = str.split('/'); 
    return new Date(date[2], date[1] ,date[0] - 1); 
} 

function GetDaysBetweenDates(date1, date2) { 
    return (date2 - date1) / (1000 * 60 * 60 * 24) 
}
