const apiURL = 'http://localhost:2005/CANVASLMS';
const orgId = 9;
const collegeId = 0;
const ipAddress = '127.0.0.1';
const macAddress = 'AA:BB:CC:DD:EE';
const userId = Number(554455);


document.addEventListener('DOMContentLoaded',()=>{

    // Accordion 1
    const ddlAuthEndpointAcc1 = document.querySelector('#ddlAuthEndpointAcc1');
    const txtClientIdAcc1 = document.querySelector('#txtClientIdAcc1');
    const txtScopeAcc1 = document.querySelector('#txtScopeAcc1');
    const txtStateAcc1 = document.querySelector('#txtStateAcc1');
    
    const authCode = document.querySelector('#authCode');

    const btnSubmitTab1 = document.querySelector('#btnSubmitTab1');
    const btnClearTab1 = document.querySelector('#btnClearTab1');

    getState(txtStateAcc1);

    checkOrgCode();
    function checkOrgCode(){
        const code = window.location.href.split('?')[1];
        console.log(code);
        if(code){
            const url = new URL(window.location.href);
            const code = url.searchParams.get("code");
            authCode.innerHTML = code;
            iziToast.success({message:`Authorization Code generated successfully.`});
        }
    }

    btnSubmitTab1.addEventListener('click',()=>{
        let postMsg = true;
        if(!txtClientIdAcc1.value){
            iziToast.error({message:`Client ID is required.`});
            txtClientIdAcc1.focus();
            postMsg = false;
            return;
        }
        if(postMsg){
            const packet = {
                "authEndpoint": ddlAuthEndpointAcc1.value,
                "clientId": txtClientIdAcc1.value,
                "scope": txtScopeAcc1.value,
                "state": txtStateAcc1.value                
            }
            fetch(`${apiURL}/getAuthorizationCode`,{
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                    },
                body: JSON.stringify(packet)
            })
            .then(response => response.json())
            .then(data => {
                    console.log(data);
                    window.location.href = data.url;                    
                })
            .catch(err => console.error(err))
        }
    });

    //get the state from the server
    function getState(element){
        fetch(`${apiURL}/getState`)
            .then(response => response.json())
            .then(data => {
                element.value = data.state;
            })
            .catch(error => console.error(error))        
    }


    // Accordion 2
    const ddlOrgIdAcc2 = document.querySelector('#ddlOrgIdAcc2');
    const ddlCollegeIdAcc2 = document.querySelector('#ddlCollegeIdAcc2');
    const ddlInstanceNameAcc2 = document.querySelector('#ddlInstanceNameAcc2');
    const ddlTokenEndpointAcc2 = document.querySelector('#ddlTokenEndpointAcc2');
    const txtClientIdAcc2 = document.querySelector('#txtClientIdAcc2');
    const txtClientSecretAcc2 = document.querySelector('#txtClientSecretAcc2');
    const txtAuthCodeAcc2 = document.querySelector('#txtAuthCodeAcc2');

    const accessToken = document.querySelector('#accessToken');
    const refreshToken = document.querySelector('#refreshToken');

    const btnSubmitAcc2 = document.querySelector('#btnSubmitAcc2');
    const btnClearAcc2 = document.querySelector('#btnClearAcc2');

    fetchCanvasInstanceValues(ddlInstanceNameAcc2);

    btnSubmitAcc2.addEventListener('click', () =>{
        let postMsg = true;
        if($(ddlOrgIdAcc2).val() <= 0){
            iziToast.error({message:`Organization is required.`});
            ddlOrgIdAcc2.focus();
            postMsg = false;
            return;
        }
        // if($(ddlCollegeIdAcc2).val() <= 0){
        //     iziToast.error({message:`College is required.`});
        //     ddlCollegeIdAcc2.focus();
        //     postMsg = false;
        //     return;
        // }
        // if($(ddlInstanceNameAcc2).val() <= 0){
        //     iziToast.error({message:`Instance is required.`});
        //     ddlInstanceNameAcc2.focus();
        //     postMsg = false;
        //     return;
        // }
        if(!txtClientIdAcc2.value){
            iziToast.error({message:`Client ID is required.`});
            txtClientIdAcc2.focus();
            postMsg = false;
            return;
        }
        if(!txtClientSecretAcc2.value){
            iziToast.error({message:`Client Secret is required.`});
            txtClientSecretAcc2.focus();
            postMsg = false;
            return;
        }
        if(!txtAuthCodeAcc2.value){
            iziToast.error({message:`Authorization Code is required.`});
            txtAuthCodeAcc2.focus();
            postMsg = false;
            return;
        }
        if(postMsg){
            const packet = {
                tokenEndpoint: ddlTokenEndpointAcc2.value,                
                clientId: txtClientIdAcc2.value,
                clientSecret: txtClientSecretAcc2.value,
                authCode: txtAuthCodeAcc2.value,
                createdBy: userId,
                ipAddress: ipAddress,
                macAddress: macAddress,
                orgId: $(ddlOrgIdAcc2).val(),
                collegeId: $(ddlCollegeIdAcc2).val(),
                canvasInstanceId: $(ddlInstanceNameAcc2).val()
            }
            fetch(`${apiURL}/getAccessAndRefreshToken`,{
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                    },
                body: JSON.stringify(packet)
            })
            .then(response => response.json())
            .then(data => {
                console.log(data)
                    accessToken.innerHTML = data.accessToken;
                    refreshToken.innerHTML = data.refreshToken; 
                    iziToast.success({message:`Token generated and stored successfully.`});            
                })
            .catch(err => console.error(err))
        }
    });

    //function to get all the Canvas Instance values
    function fetchCanvasInstanceValues(element){
        fetch(`${apiURL}/getCanvasInstance/${orgId}/${collegeId}`)
        .then(response => response.json())
        .then(data =>{   
            console.log(data);            
            RenderDropDown($(element), data);                       
        })
        .catch(error => console.error(error));      
    }
});


var RenderDropDown = function (control, data) {
    control.empty();  
    if(data.length == 0){
        control.prepend($("<option/>").val(-1).text("No records found."));
    }
    else{
        $.each(data, function () {
            control.append($("<option />").val(this.Value).text(this.Text));
        }); control.prepend($("<option selected/>").val(0).text("Please Select"));
    }
};