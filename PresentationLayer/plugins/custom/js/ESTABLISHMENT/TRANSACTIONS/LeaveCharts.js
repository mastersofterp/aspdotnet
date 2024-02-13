
document.addEventListener("DOMContentLoaded",()=>{
    //console.log('hitting some thing')
    fetchGraphData()
         .then(() => {
             console.log('Execution done');
            
                    })
        .catch(error => {
            console.error('An error occurred:', error);
            });

    
});

function testQuery(query, data){
    var result = alasql(query, [data]);
    //console.log(result);
}

$("#ddlDashborad").change(function () {
    fetchGraphData()
     .then(() => {
         console.log('Execution done');
            
})
        .catch(error => {
            console.error('An error occurred:', error);
});
});

var obj = {};

obj.collgeid = 0,
obj.Staff = 0
obj.Dept = 0
//obj.Desig = 0,
//obj.Payrul = 0,
//obj.Dashtype = $('#ddlDashborad').val()

function fetchGraphData() {
   
    return new Promise((resolve, reject) => {
        // give URL here
        
        fetch('../TRANSACTIONS/LeaveChart.aspx/GetCourseGraph', {
            //  fetch('/Academic/Dashboard/Courses_Analytics.aspx/GetCourseGraph', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json', // Set the content type
        },
        body: JSON.stringify(obj), // Convert data to JSON
    }).then(response => {
              if (!response.ok) {
              throw new Error('Network response was not ok');
}
return response.json();
})
.then(data => {
    // Handle the successful response
    //if ($('#ddlDashborad').val() == 0) {
                BindGraph(data.d);
               
        //}
        //if ($('#ddlDashborad').val() == 1) {
        //    BindGraphSalary(data.d);
            
        //}

resolve();
})
 .catch(error => {
    // Reject the promise with the error
                console.error(error);
                reject(error);
            });
});
}

function BindGraph (data) {
    //console.log('str')

    //var data = [
    //{ Status: 'Pending' }   
    //];
           
    plotFilteredGraphs({
        

        dashboardName: 'Leave Charts',
        graphArea: '#graph-plot',
        filterArea: '#filter-area',
        cardArea: "#card-area",
        data: data,
        buttons: ['reset', 'print'],
        elements:[
            {
                title: "College Wise Apply Leave",
                width: "col-lg-6 col-sm-6 col-12 in-down a3",
                type: "bar",
                elementType: "graph",
                query: "SELECT DISTINCT COLLEGE_CODE AS data_label, COUNT(DISTINCT letrno) AS data_val0 FROM ? GROUP BY COLLEGE_CODE",
                chartSettings: {
                    indexAxis: 'x',
                    legend: true,
                    //scales: 'stackedGraph',
                    scrollSize: 10,
                    scrollButtons: true,
                    // layoutPadding: [60, 0],
                    legendMargin:false,
                    usePointStyle: true
                },
                // height: '350px'
            },
                              
            {
                title: "Leave Name Wise Apply Leave",
                width: "col-lg-6 col-sm-6 col-12 in-down a5",
                type: "bar",
                elementType: "graph",
                query: "SELECT  DISTINCT LeaveName AS data_label, COUNT(DISTINCT letrno) AS data_val0 FROM ? GROUP BY LeaveName",
                chartSettings: { 
                    indexAxis: 'y',
                    legend: false,   
                    //scales: 'stackedGraph'                         
                }
            }, 
            {
                title: "Month wise Apply Leave",
                width: "col-lg-6 col-sm-6 col-12 in-down a7",
                type: "bar",
                elementType: "graph",
                query: "SELECT DISTINCT Mname AS data_label, COUNT(DISTINCT letrno) AS data_val0 FROM ? GROUP BY Mname",
                chartSettings: { 
                    indexAxis: 'x',
                    legend: false,   
                    //scales: 'stackedGraph'                         
                }
            }, 

            {
                title: "Leave Status",
                width: "col-lg-6 col-sm-6 col-12 in-down a9",
                type: "bar",
                elementType: "graph",
                query: "SELECT DISTINCT Status AS data_label, COUNT(DISTINCT letrno) AS data_val0 FROM ? GROUP BY Status",
                chartSettings: { 
                    indexAxis: 'y',
                    legend: false,   
                    //scales: 'stackedGraph'                         
                }
            }, 
                              
            {
                title: "Colleges",
                width: "col-lg-3 col-md-3 col-6 in-down a2",
                elementType: "card",
                icon: "bi-building",
                color: '#297ffd',
                query: "SELECT COUNT(DISTINCT COLLEGE_CODE) AS card_val FROM ?"
            },
            {
                title: "Total Applied",
                width: "col-lg-3 col-md-3 col-6 in-down a5",
                elementType: "card",
                icon: "bi-book", //optional
                color: "#2ad0f2", //optional
                query: "SELECT COUNT(DISTINCT letrno) AS card_val FROM ? "
            }
            //{
            //    title: "Total Pending",
            //    width: "col-lg-3 col-md-3 col-6 in-down a5",
            //    elementType: "card",
            //    icon: "bi-book", //optional
            //    color: "#2ad0f2", //optional
            //    query: "SELECT COUNT(DISTINCT letrno) AS card_val FROM ? ",[data]
            //}

        ],                
        // needs to change the 
        filters:[

            {
                title: "College",
                field: "COLLEGE_CODE"
                //width: "col-lg-12 col-md-12 col-12",
                //height: "100px"
            },
            {
                title: "Staff Type",
                field: "STAFF"
            },
            {
                title: "Department",
                field: "SUBDEPT"
            }
            //{
            //    title: "Designation",
            //    field: "SUBDESIG"
            //},
            //{
            //    title: "Rule",
            //    field: "RULENAME"
            //}
        ]
    });
}


function BindGraphSalary (data) {
    //console.log('str')
           
    plotFilteredGraphs({
        
        dashboardName: 'Payroll Salary Charts',
        graphArea: '#graph-plot',
        filterArea: '#filter-area',
        cardArea: "#card-area",
        data: data,
        buttons: ['reset', 'print'],
        elements:[
            {
                title: "College Wise Salary",
                width: "col-lg-6 col-sm-6 col-12 in-down a3",
                type: "bar",
                elementType: "graph",
                query: "SELECT DISTINCT COLLEGE_CODE AS data_label, COUNT(DISTINCT IDNO) AS data_val0 FROM ? GROUP BY COLLEGE_CODE",
                chartSettings: {
                    indexAxis: 'x',
                    legend: true,
                    //scales: 'stackedGraph',
                    scrollSize: 10,
                    scrollButtons: true,
                    // layoutPadding: [60, 0],
                    legendMargin:false,
                    usePointStyle: true
                },
                // height: '350px'
            },
                              
            {
                title: "Staff Wise Salary",
                width: "col-lg-6 col-sm-6 col-12 in-down a5",
                type: "bar",
                elementType: "graph",
               // var res = alasql('SELECT a, SUM(b) AS b FROM ? GROUP BY a',[data]);
                query: "SELECT  DISTINCT STAFF AS data_label, SUM(DISTINCT NETPAY) AS data_val0 FROM ? GROUP BY STAFF",
                chartSettings: { 
                    indexAxis: 'y',
                    legend: false,   
                    //scales: 'stackedGraph'                         
                }
            }, 
            {
                title: "Department wise Salary",
                width: "col-lg-6 col-sm-6 col-12 in-down a7",
                type: "bar",
                elementType: "graph",
                query: "SELECT DISTINCT SUBDEPT AS data_label, SUM(NET_PAY) AS data_val0 FROM ? GROUP BY SUBDEPT",
                chartSettings: { 
                    indexAxis: 'y',
                    legend: false,   
                    //scales: 'stackedGraph'                         
                }
            }, 

            
                              
            {
                title: "Colleges",
                width: "col-lg-3 col-md-3 col-6 in-down a2",
                elementType: "card",
                icon: "bi-building",
                color: '#297ffd',
                query: "SELECT COUNT(DISTINCT COLLEGE_CODE) AS card_val FROM ?"
            },
            {
                title: "Staff",
                width: "col-lg-3 col-md-3 col-6 in-down a5",
                elementType: "card",
                icon: "bi-book", //optional
                color: "#2ad0f2", //optional
                query: "SELECT COUNT(DISTINCT STAFF) AS card_val FROM ? "
            }
        ],                
        // needs to change the 
        filters:[

            {
                title: "College",
                field: "COLLEGE_CODE"
                //width: "col-lg-12 col-md-12 col-12",
                //height: "100px"
            },
            {
                title: "Staff",
                field: "STAFF"
            },
            {
                title: "Department",
                field: "SUBDEPT"
            },
            {
                title: "Designation",
                field: "SUBDESIG"
            }
        ]
    });
}


//.then(data => {
           

//            plotFilteredGraphs({
//                dashboardName: 'Test Report',
//                graphArea: '#graph-plot',
//                filterArea: '#filter-area',
//                cardArea: "#card-area",
//                data: data,
//                buttons: ['reset', 'download', 'print', 'pivot'],
//                elements:[
//                    {
//                        title: "Department wise applied leaves",
//                        width: "col-lg-6 col-md-6 col-12 temp-height",
//                        type: "doughnut",
//                        elementType: "graph",
//                        query: "SELECT DISTINCT Department AS data_label, SUM(No_of_days) AS data_val0, SUM(No_of_days) AS data_val1 FROM ? GROUP BY Department",
//                        chartSettings: {
//                            indexAxis: 'x',
//                            legend: true,
//                            scales: 'stackedGraph',
//                            scrollSize: 10,
//                            scrollButtons: true,
//                            // layoutPadding: [60, 0],
//                            legendMargin:false,
//                            usePointStyle: true
//                        },
//                        // height: '350px'
//                    },
//                    {
//                        // title: "test",
//                        width: "col-lg-6 col-md-6 col-12",
//                        initFilterTable: true,
//                        elementType: "table",
//                        query: `
//                        SELECT 
//                            Department, 
//                            EmpName, 
//                            SUM(No_of_days) AS Applied,
//                SUM(CASE WHEN Status='Approved' THEN 1 ELSE 0 END) AS Approved,
//                            SUM(CASE WHEN Status='Pending' THEN 1 ELSE 0 END) AS Pending,
//                            SUM(CASE WHEN Status='Rejected' THEN 1 ELSE 0 END) AS Rejected,
//                            LEAVE_REMARKS AS Remarks 
//                        FROM ?
//                        GROUP BY Department, EmpName 
//ORDER BY Department, EmpName
//`,
//height: '250px'
//},          
//{
//    title: "Monthly applied",
//    width: "col-lg-6 col-md-6 col-12",
//    type: "bar",
//    elementType: "graph",
//    query: `
//    SELECT 
//    CONCAT(MID(FDate, 1, 4), "-", MID(FDate, 6, 2)) AS data_label,
//    SUM(No_of_days) AS data_val0
//    FROM ?
//    GROUP BY
//    CONCAT(MID(FDate, 1, 4), "-", MID(FDate, 6, 2))
//    ORDER BY
//    data_label
//`,
                        
//    },
//{
//title: "Type wise Leaves",
//width: "col-lg-6 col-md-6 col-12",
//type: "bar",
//elementType: "graph",
//query: "SELECT  LeaveName AS data_label, COUNT(*) AS data_val0, COUNT(*) AS data_val1 FROM ? GROUP BY LeaveName",
//chartSettings: {                            
//    legend: false,   
//    scales: 'stackedGraph'                         
//}
//},   
                              
//{
//    title: "Applied Leaves",
//    width: "col-lg-3 col-md-3 col-12",
//    elementType: "card",
//    query: "SELECT SUM(No_of_days) AS card_val FROM ?"
//},
//{
//title: "Approved Leaves",
//width: "col-lg-3 col-md-3 col-12",
//elementType: "card",
//icon: "bi-0-square", //optional
//color: "#ffc600", //optional
//query: "SELECT SUM(No_of_days) AS card_val FROM ? WHERE Status='Approved'"
//},                    
//{
//    title: "Pending Leaves",
//    width: "col-lg-3 col-md-3 col-12",
//    elementType: "card",
//    query: "SELECT SUM(No_of_days) AS card_val FROM ? WHERE Status='Pending'"
//},
//{
//title: "Rejected Leaves",
//width: "col-lg-3 col-md-3 col-12",
//elementType: "card",
//query: "SELECT SUM(No_of_days) AS card_val FROM ? WHERE Status='Rejected'"
//},
//{
//    title: "Today On Leave",
//    width: "col-lg-3 col-md-3 col-12",
//    elementType: "card",
//    query: `SELECT COUNT(EmpName) AS card_val FROM ? WHERE DATE('${moment().format('YYYY/MM/DD')}') >= DATE(FDate) AND DATE('${moment().format('YYYY/MM/DD')}') <= DATE(To_date)`
//},
//{
//title: "Tomorrow On Leave",
//width: "col-lg-3 col-md-3 col-12",
//elementType: "card",
//query: `SELECT COUNT(EmpName) AS card_val FROM ? WHERE DATE('${moment().add(1, 'day').format('YYYY/MM/DD')}') >= DATE(FDate) AND DATE('${moment().add(1, 'day').format('YYYY/MM/DD')}') <= DATE(To_date)`
//}
//],                
//// needs to change the 
//filters:[
//    {
//        title: "Date",
//        fields: ['FDate','To_date'],
//        isDate: true,                        
//    },
//    {
//        title: "Status",
//        field: "Status",
//        width: "col-lg-12 col-md-12 col-12",
//        height: "100px"
//    },
//    {
//        title: "Department",
//        field: "Department"
//    },
//    {
//        title: "Leave Type",
//        field: "LeaveName"
//    },
//    {
//        title: "Staff Type",
//        field: "StaffType"
//    },
//    {
//        title: "Employee",
//        field: "EmpName"
//    }
//]
//});

//})