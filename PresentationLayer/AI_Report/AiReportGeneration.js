document.addEventListener('DOMContentLoaded', function () {

    

    const ApiUrl = "https://pythonapi.mastersofterp.in/RFARS/"
    //const ApiUrl = "http://172.16.4.110:8988/"
    const btnSend = document.querySelector('#btnSend1')
    const chatTextarea = document.querySelector(".chat-textarea")
    var cnt = 1

    const descArray = [{
        id:0,
        desc: "No Desc Found"
    },
    {
        id:1,
        desc: "You can ask the AI questions regarding the following fields: 'REGISTRATION_NUMBER, STUDENT_NAME, STUDENT_DATE_OF_BIRTH, GENDER, RELIGION, NATIONALITY, BLOOD_GROUP,ANNUAL_FAMILY_INCOME, PHYSICALLY_HANDICAPPED, STUDENT_EMAIL_ID, STUDENT_MOBILE_NUMBER, PROGRAM_LEVEL, PROGRAM_CATEGORY, DEGREE, ADMISSION_SESSION, LAST_QUALIFYING_EXAM, PERCENTAGE_IN_LAST_QUALIFYING_EXAM, PERMANENT_ADDRESS, PERMANENT_STATE, PERMANENT_DISTRICT, PERMANENT_PINCODE in this report."
    }]

    //doBS()
    //initServer()

    btnSend.addEventListener('click', () => {
        cnt = cnt + 1   
        addChatArea(cnt) 
    var prompt = document.getElementById(`txtChat1`).value
    sendPrompt(prompt)
    //  getAPI()
})

async function getAPI() {
    const response = await fetch(`${ApiUrl}chat`);
var data = await response.json();
allData.push(...data['data']);
console.log(allData)
// console.log(allData)
// console.table(data['data']);    
// const table= addOptionForFiltering('filterTable');
// console.table(allData);
//table name , column names to show in select , columnsNames from procedure to display
initializeCustomFilter(
    'resultTable',
    ['Course ID', 'Course Code', 'Course Name', 'Course Short Name',
        'Course Credits', 'Course Category', 'Course Type', 'Department', 'Status',], ['SubjectCode', 'SubjectName', 'SubjectShortName', 'SubjectCredit', 'SubjectCategory', 'SubjectTypeName', 'DepartmentName', 'Status']);
initializePivotTable('resultTable');
}
$('#ddlReportType').on('change', function() {
    var value = document.getElementById('ddlReportType').value

    var packet = {
        context_choice : value
    }
   
    if(value > 0 ){
        console.log(value)
        fetch(`${ApiUrl}choose-context`, { //choose_context
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer uGTj66kW9oy7NBQjlK9AruXAn0aPHPZkvdGtcavFSks'
            },
            body: JSON.stringify(packet)
        }).then(response => response.json()).then(data => {
            console.log('Success: it is', data.all_columns);
    iziToast.success({ message: data.message })
const descBox = document.querySelector('#report-desc');
const filteredDesc = descArray.filter(obj => obj.id == value);
    descBox.innerHTML = `
        <label>Description: <code></code></label>
        <div>${filteredDesc[0].desc}</div>
    `;
    document.getElementById('tableContent').innerHTML = ""
    makeColumns(data.all_columns)
}).catch((error) => {
    console.error('Error:', error);
iziToast.error({ message: error });
});
}
else{
    // remove the table and the desc
    document.getElementById('tableColumns').innerHTML = ''
    const descBox = document.querySelector('#report-desc');
    const filteredDesc1 = descArray.filter(obj => obj.id == value);
    descBox.innerHTML = `
        <label>Description: <code></code></label>
        <div>${filteredDesc1[0].desc}</div>
    `;

}

})

// ddlReportType

function addChatArea(count) {
    var div = document.createElement('div');
    div.className = "d-flex align-items-center"
    div.innerHTML = `
                            <div class="form-group w-100">
                                <textarea class="form-control" id="txtChat${count}" name="txtChat" tabindex="1" spellcheck="true" placeholder="Enter Your Query "></textarea>
                            </div>
                            <button type="button" tabindex="1" class="btn btn-primary ms-2" id="btnSend${count}">Send</button>
    `
    chatTextarea.append(div);

    document.getElementById(`btnSend${count}`).addEventListener('click', function() {
        cnt = cnt + 1   
        addChatArea(cnt)
        var prompt = document.getElementById(`txtChat${count}`).value
sendPrompt(prompt)
})

// removing previous send button
document.getElementById(`btnSend${count-1}`).setAttribute("style", "display: none")

// scrooling to the botom of the textArea
chatTextarea.scrollTop = chatTextarea.scrollHeight
}

function makeColumns(columns) {
    document.getElementById('tableColumns').innerHTML = ''
    columns.forEach(column => {
        var th = document.createElement('th')
        th.innerHTML = column
        th.setAttribute('data-field', column)
    document.getElementById('tableColumns').appendChild(th)
})
}

function populateTable(entry) {
    var entryArray = Object.values(entry).reverse()
    var tr = document.createElement('tr')
    entryArray.forEach( data => {
        var td = document.createElement('td')
        td.innerHTML = data
        tr.appendChild(td)
})
    document.getElementById("tableContent").appendChild(tr)
}

function createBootstrapTable(tableData) {
    var oTable = $('#resultTable').bootstrapTable('destroy').bootstrapTable(bootstrapTableSettings(tableData));
    function bootstrapTableSettings(data, pagination=true) {
        const tableSettings = {
            destroy: true,
            search: true,
            pagination: pagination,
            sortable: true,
            showColumns: true,                  //Whether to display all columns (select the columns to display)
            //   showRefresh: true,
            showToggle: true,
            exportDataType: "all",
            minimumCountColumns: 2,
            filterControl: true,
            showExport: true,
            showColumnsToggleAll: true,
            filterControlVisible: false,
            //filterControlContainer: true,
            showFilterControlSwitch: false,
            showFilterControlSwitch: true,
            advancedSearch: true,
            advancedSearch: true,
            data: data,
            exportTypes: ['doc', 'excel'],
            pageList: "[10, 25, 50, 100, all]",
        }
        return tableSettings;
    }
    }

    function sendPrompt(input) {
        console.log(input)
        var packet = {
            user_message: input
        }

        fetch(`${ApiUrl}chat`, { //choose_context
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer uGTj66kW9oy7NBQjlK9AruXAn0aPHPZkvdGtcavFSks'
            },
            body: JSON.stringify(packet)
        })
        .then(response => response.json())
        .then(data => {
            reInitTable();
    console.log('Success:', data);
    document.getElementById('tableContent').innerHTML = ""
    makeColumns(Object.keys(data[0]).reverse())
    createBootstrapTable(data)

    allData.push(...data);
    console.log(allData)
    // console.log(allData)
    // console.table(data['data']);
    // const table= addOptionForFiltering('filterTable');
    // console.table(allData);
    //table name , column names to show in select , columnsNames from procedure to display
    //  setTimeout(function () {
            
    var columnArray = Object.keys(data[0])
    console.log(columnArray)
    initializeCustomFilter(
        'resultTable',
        columnArray, columnArray);
    initializePivotTable('resultTable');
    //   }, 2000);  
           
    // data.forEach(entry => {
    //     populateTable(entry)
    // })
})
.catch((error) => {
    console.error('Error:', error);
iziToast.error({ message: error });
});
}

function initServer() {

    fetch(`${ApiUrl}set_hardcoded_user_session`, { //choose_context
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (response.status == 200) {
                iziToast.success({message : "Session Initialized"})
}
else {
    iziToast.error({message : "an error occurred, please contact Swarnim Barapatre. contact : +91 814 983 3469"})
}
})
        .catch((error) => {
            console.error('Error:', error);
iziToast.error({ message: error });
});
}

function doBS() {
    // register

    var packet = {
        username : "vedant",
        password : "wakandaforever"
    }

    fetch(`${ApiUrl}register`, { //choose_context
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(packet)
    })
        .then(response => response.json())
        .then(data => {
            console.log("registrered: " , data)
            iziToast.success({message : "registered"})

var packet = {
    username : "vedant",
    password : "wakandaforever"
}
    
fetch(`${ApiUrl}login`, { //choose_context
method: 'POST',
headers: {
    'Content-Type': 'application/json'
},
body: JSON.stringify(packet)
})
                .then(response => response.json())
                .then(data => {
                    console.log(data);
iziToast.success({message : "loginned"})
// var packet = {
    
// }
            
// fetch(`${ApiUrl}logout`, { //choose_context
//     method: 'POST',
//     headers: {
//         'Content-Type': 'application/json'
//     },
//     body: JSON.stringify(packet)
// })
//     .then(response => response.json())
//     .then(data => {
//         console.log(data);
//         iziToast.success({message : "logged out"})
//     })
//     .catch((error) => {
//         console.error('Error:', error);
//         iziToast.error({ message: error });
//     });
})
                .catch((error) => {
                    console.error('Error:', error);
iziToast.error({ message: error });
});
})
        .catch((error) => {
            console.error('Error:', error);
iziToast.error({ message: error });
});
// login




}

function reInitTable() {
    document.getElementById("theTable").innerHTML = `
                                <div id="toolbar" class='tableToolbarLeft'>
                            </div>
                            <table id="resultTable" class="table table-striped table-bordered display">
                                <thead>
                                    <tr id="tableColumns">
                                    </tr>
                                </thead>
                                <tbody id="tableContent">
                                </tbody>

                            </table>
    ` 
}

})