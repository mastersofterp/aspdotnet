



let isAdmin = true;

let addMoreCommunityBtn;  
let userName;
let userImage;
let uploadCommunityPostFlag = false;
let uploadImagesToMongoBlobFlag = false;
let uploadVideosToMongoBlobFlag = false;
let uploadEventToMongoBlobFlag = false;
let checkedCommunityEventRadio = 'eventImageUpload';

//community
let communityIconGroup;
let allCommunityListColors;
let allCommunityListIcons;
let newCommunityIcon;
let editCommunityProperties;
let communityContainer;
let editCommunityBtn;
let removeCommunityEditDetailsBtn;
let editAllCommunityBtn;
let uploadedImage;
let generalCommunity;
let allCommunityNames = [];
let communityIncahrgeDD;
let communityDisplayName;
let selectedCommunityId;
let inchargeFor = [];
let modalPostContainer;
let modalEventContainer;
let previewImagesContainer;
let communityImagePostContent;
let communityVideoPostContent;
let communityEventVideoPostContent;
let communityPollForm;
let communityPollPostContent;
let communityPollContainer;
let uploadPostsToCommunityBtn;
let communityMyPollsContainer;
let modalPollContainer;
let searchAllCommunitiesBtn;
let allCommunityBody;
let allMembersIC;

//file upload
let currentFilePointer = 0;
let maxBlockSize = 256 * 1024 * 1000 * 10000;
let userImageInput;
let userVideoInput;
let selectedCommunityImage;
let selectedCommunityVideo;
let fileName;
let fileSize;
let fileType;
let fileItem;
let allImageFiles;
let allVideoFiles;
let allEventVideoFiles;
let noOfFiles;
let uploadFilesToCommunityBtn;
let uploadVideosToCommunityBtn;
let imageFileNamesToUploadToCommunity = [];
let communityPreviewContainerCount = 1;
let communityEventPreviewContainerCount = 1;
let allSelectedFiles = [];
let selectedVideoName;
let imageIndexForUploadInCommunity = 0;
let imageEventIndexForUploadInCommunity = 0;
let communityImageForm;
let communityVideoForm;
let communityVideoPlayer;
let communityEventVideoPlayer;
let userEventVideoInput;
let previewEventVideoContainer;
let previewEventImagesContainer;
let userEventImageInput;
let uploadEventToCommunityBtn;
let allEventImageFiles;
let selectedCommunityEventImage;
let selectedCommunityEventVideo;
let selectedEventVideoName;
let imageEventFileNamesToUploadToCommunity = [];

//news
let editNewsBtn;
let removeNewsEditDetailsBtn;
let newsContainer;
let addMoreNewsBtn;
let communityModalTitle;
let communityNewsModalDiv;
let postsContainer;

//members
let communityMemberContainer;
let allMembersOA = [];
let memberContId;
let CurrentMemberName;
//********************************************************** */
const communityBlobContainer = 'https://crescentdocstorage.blob.core.windows.net/community-data';
//const communityURL = "http://172.24.96.1:5520/Community";
//const communityNewsURL = "http://172.24.96.1:5521/CommunityNews";
//const communityMemberURL = "http://172.24.96.1:5525/CommunityMembers";
const communityURL = "https://api.mastersofterp.in/RFCRESCS/Community";
const communityNewsURL = "https://api.mastersofterp.in/RFCRESCNS/CommunityNews";
const communityMemberURL = "https://api.mastersofterp.in/RFCRESCMS/CommunityMembers";
//********************************************************** */
const allCommunityIconList = [
    'bi-list-task',
    "bi-gift-fill",
    "bi-hospital-fill",
    "bi-airplane-fill",
    "bi-building",
    "bi-cup-straw",
    'bi-gem',
    "bi-umbrella-fill",
    "bi-joystick",
    "bi-train-freight-front-fill",
    "bi-alarm-fill",
    "bi-archive-fill",
    "bi-award-fill",
    "bi-asterisk",
    "bi-back",
    "bi-bag-check-fill",
    "bi-bag-fill",
    "bi-bag-plus-fill",
    "bi-bag-x-fill",
    "bi-balloon-fill",
    "bi-bandaid-fill",
    "bi-bar-chart-fill",
    "bi-bar-chart-steps",
    "bi-basket-fill",
    "bi-binoculars-fill",
    "bi-book-fill",
    "bi-book-half",
    "bi-bookmark-check-fill",
    "bi-bookmark-star-fill",
    "bi-boombox-fill",
    "bi-border",
    "bi-border-all",
    "bi-bounding-box",
    "bi-box-fill",
    "bi-box2-heart-fill",
    "bi-boxes",
    "bi-braces",
    "bi-braces-asterisk",
    "bi-code-slash",
    "bi-code",
    "bi-bricks",
    "bi-briefcase-fill",
    "bi-brightness-high-fill",
    "bi-broadcast-pin",
    "bi-bug-fill",
    "bi-calendar-check-fill",
    "bi-calendar-date-fill",
    "bi-calendar-day-fill",
    "bi-calendar-month-fill",
    "bi-calendar-range-fill",
    "bi-camera-fill",
    "bi-camera-reels-fill",
    "bi-camera-video-off-fill",
    "bi-capsule",
    "bi-card-checklist",
    "bi-card-image",
    "bi-cassette-fill",
    "bi-chat-left-dots-fill",
    "bi-check-square-fill",
    "bi-clipboard-check-fill",
    "bi-clipboard-data-fill",
    "bi-clipboard-fill",
    "bi-clipboard-x-fill",
    "bi-clipboard-plus-fill",
    "bi-clipboard-minus-fill",
    "bi-cloud-fill",
    "bi-cone-striped",
    "bi-cpu-fill",
    "bi-credit-card-fill",
    "bi-currency-dollar",
    "bi-cursor-fill",
    "bi-diagram-3-fill",
    "bi-dpad-fill",
    "bi-envelope-fill",
    "bi-file-earmark-fill",
    "bi-file-earmark-ruled-fill",
    "bi-file-earmark-zip-fill",
    "bi-gear-fill",
    "bi-globe2",
    "bi-headset",
    "bi-hourglass-split",
    "bi-house-door-fill",
    "bi-laptop-fill",
    "bi-map-fill",
    "bi-megaphone-fill",
    "bi-mic-fill",
    "bi-mortarboard-fill",
    "bi-music-note-beamed",
    "bi-palette-fill",
    "bi-pc-display-horizontal",
    "bi-pencil-fill",
    "bi-person-check-fill",
    "bi-person-fill",
    "bi-pin-angle-fill",
    "bi-recycle",
    "bi-share-fill",
    "bi-shield-fill-exclamation",
    "bi-stickies-fill",
    "bi-x-square-fill",
    "bi-square-fill",
];

/**
 * Document Ready
 * TODO: change it so that it loads only once the button is pressed
 */
CurrentMemberName = $('#ctl00_lblLink').text(); 
document.addEventListener('DOMContentLoaded',()=>{

    document.querySelector('.community-toggle').addEventListener('click', () => {
        // stuff that only the admin can do
        const uiserTypeId = document.querySelector('#userTypeId').value;

collegeId = document.querySelector('#CollegeID').value;
orgId = document.querySelector('#orgID').value;
userId = Number(document.querySelector('#userID').value);
uiserTypeId == 1 ? isAdmin = true : isAdmin = false;

ipAddress = document.querySelector('#IpAddress').value;
macAddress = document.querySelector('#MacAddress').value;



//document.addEventListener('DOMContentLoaded',()=>{


// **************************************

//    inchargeId = 1;
//collegeId = 2;
//orgId = 2;
//userId = 1;
//ipAddress = '1.1.1.1';
//macAddress = '0:0:0:0:0:0';
//    const uiserTypeId = 11;
//uiserTypeId == 11 ? isAdmin = true : isAdmin = false;

communityContainer = document.querySelector('.community-container');

if (isAdmin) {

    makeModalForAddingCommunity();

    editCommunityBtn = document.querySelector('#editCommunityItem');
    editCommunityBtn.addEventListener('click', makeAllCommunityEditable);

    removeCommunityEditDetailsBtn = document.querySelector('#removeCommunityEditDetails');
    removeCommunityEditDetailsBtn.addEventListener('click', removeEditableCommunity);

    editAllCommunityBtn = document.querySelector('#editCommunityItem');
    editAllCommunityBtn.classList.remove('hide');
    editAllCommunityBtn.addEventListener('click', makeAllCommunityEditable);

    addMoreCommunityBtn = document.querySelector('#addMoreCommunityItems');
    addMoreCommunityBtn.classList.remove('hide');
    addMoreCommunityBtn.addEventListener('click', makeNewCommunityModal);

    //news
    makeModalToAddNews();

    addMoreNewsBtn = document.querySelector('#addMoreNewsItems');
    addMoreNewsBtn.classList.remove('hide');

    editNewsBtn = document.querySelector('#editNewsItem');
    editNewsBtn.classList.remove('hide');
    editNewsBtn.addEventListener('click', makeEditableNews);

    removeNewsEditDetailsBtn = document.querySelector('#removeNewsEditDetails');
    removeNewsEditDetailsBtn.addEventListener('click', removeEditableNews);

    /**
     * TODO: Store news in a different mongo db collection
     */
}
//get user info
userName = document.querySelector('#currentUsersName').textContent.trim();

document.querySelector('#profileAvtar') ? userImage = document.querySelector('#profileAvtar').src : userImage = '';
//end user info
communityMemberContainer = document.querySelector('.member-container ul');

searchAllCommunitiesBtn = document.querySelector('#searchAllCommunities');

makeAllCommunitiesModal();
searchAllCommunitiesBtn.addEventListener('click', () => {

});

getAllMembersInCommunity();

checkIfANewMember();

    const communityUserImage = document.querySelector('#community-user-image');
communityUserImage.src = document.querySelector('#profileAvtar') ? document.querySelector('#profileAvtar').src : 'error';

    const communityUserSVG = document.querySelector('#community-user-image-svg');

communityUserImage.onerror = () => {
    communityUserImage.classList.add('hide');
communityUserSVG.dataset.jdenticonValue = userName;
communityUserSVG.classList.remove('hide');
jdenticon();
}

newsContainer = document.querySelector('.news-container');

makeDisplayModalForNews();

getAllNewsForCollege();

generalCommunity = document.querySelector('#generalCommunity');

getAllCommunityNames();

postsContainer = document.querySelector('.posts-container');

communityDisplayName = document.querySelector('#communityDisplayName');



makeCommunityImageModal();
//image
previewImagesContainer = document.querySelector('#previewCommunityImages');

userImageInput = document.getElementById("community-user-image-upload");

userImageInput.addEventListener('change', (e) => {
    allImageFiles = e.target.files;
previewImagesContainer.querySelector('.community-image-upload-preview').classList.add('hide');
[...e.target.files].forEach(previewCommunityImages);
});

uploadFilesToCommunityBtn = document.querySelector('#upload-files-community');

makeCommunityVideoModal();
//video
previewVideoContainer = document.querySelector('#previewCommunityVideo');

userVideoInput = document.getElementById("community-user-video-upload");

communityVideoPlayer = previewVideoContainer.querySelector('#community-video-preview');

userVideoInput.addEventListener('change', (e) => {
    allVideoFiles = e.target.files;
fileSize = allVideoFiles[0].size;
if (fileSize > 262144000) {
    iziToast.error({ message: 'Video cannot exceed 250MB!' });
    allVideoFiles = '';
    communityVideoForm.reset();
    return;
}
communityVideoPlayer.classList.remove('hide');
previewVideoContainer.querySelector('.community-video-upload-preview').classList.add('hide');
previewCommunityVideo();
});

uploadVideosToCommunityBtn = document.querySelector('#upload-video-community');

makeCommunityEventModal();
//event video
previewEventVideoContainer = document.querySelector('#previewCommunityEventVideo');

userEventVideoInput = document.querySelector('#community-user-event-video-upload');

communityEventVideoPlayer = document.querySelector('#community-event-video-preview');

userEventVideoInput.addEventListener('change', (e) => {
    allEventVideoFiles = e.target.files;
if (fileSize > 262144000) {
    iziToast.error({ message: 'Video cannot exceed 250MB!' });
    allEventVideoFiles = '';
    communityEventVideoForm.reset();
    return;
}
communityEventVideoPlayer.classList.remove('hide');
previewEventVideoContainer.querySelector('.community-video-upload-preview').classList.add('hide');
previewCommunityEventVideo1();
});

//event image
previewEventImagesContainer = document.querySelector('#previewCommunityEventImages');

userEventImageInput = document.getElementById("community-user-event-image-upload");

userEventImageInput.addEventListener('change', (e) => {
    allEventImageFiles = e.target.files;
previewEventImagesContainer.querySelector('.community-image-upload-preview').classList.add('hide');
[...e.target.files].forEach(previewCommunityEventImages);
});

uploadEventToCommunityBtn = document.querySelector('#upload-event-community');

makeCommunityPollModal();


$(document).ajaxStop(function () {
    /**
     * TODO: make the function to store the filenames in mongo
     */
    if (uploadImagesToMongoBlobFlag) {
        const packet = {
            postDescription: communityImagePostContent.value,
            filePath: imageFileNamesToUploadToCommunity,
            creationDateAndTime: toIsoString(new Date()),
            ipAddress: ipAddress,
            macAddress: macAddress,
            createdBy: userId,
            noOfLikes: [],
            comments: []
        }
        $.ajax({
            url: `${communityURL}/addNewPost/${selectedCommunityId}/${userId}`,
    method: "PATCH",
    data: JSON.stringify(packet),
    contentType: "application/json",
    success: function (data) {
        uploadFilesToCommunityBtn.innerHTML = `Post`;
        allImageFiles = '';
        communityImageForm.querySelector('.community-image-upload-preview').classList.remove('hide');
        communityImageForm.querySelectorAll('.image-preview-grid').forEach(div => div.innerHTML = '');
        communityPreviewContainerCount = 1;
        imageIndexForUploadInCommunity = 0;
        communityImageForm.reset();
        uploadImagesToMongoBlobFlag = false;
        $('#communityImageUploadModal').modal('hide');
        // if(modalPostContainer){
        //     packet._id = data.id
        //     populatePostsData(packet,modalPostContainer);
        // }
        Swal.fire(
            'Post Added!',
            'Your community administrators have been notified, once they approve your post it will be visible to all members in the community.',
            'success'
        );
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        uploadImagesToMongoBlobFlag = false;
        console.error('error', XMLHttpRequest)
    }
});
}

else if (uploadVideosToMongoBlobFlag) {
    const packet = {
        postDescription: communityVideoPostContent.value,
        filePath: [selectedVideoName],
        creationDateAndTime: toIsoString(new Date()),
        ipAddress: ipAddress,
        isVideo: true,
        macAddress: macAddress,
        createdBy: userId,
        noOfLikes: [],
        comments: []
    }
    $.ajax({
        url: `${communityURL}/addNewPost/${selectedCommunityId}/${userId}`,
method: "PATCH",
    data: JSON.stringify(packet),
contentType: "application/json",
success: function (data) {
    uploadVideosToCommunityBtn.innerHTML = `Post`;
    allVideoFiles = '';
    communityVideoForm.querySelector('.community-video-upload-preview').classList.remove('hide');
    communityVideoForm.reset();
    communityVideoPlayer.classList.add('hide');
    uploadVideosToMongoBlobFlag = false;
    $('#communityVideoUploadModal').modal('hide');
    Swal.fire(
        'Post Added!',
        'Your community administrators have been notified, once they approve your post it will be visible to all members in the community.',
        'success'
    );
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    uploadVideosToMongoBlobFlag = false;
    console.error('error', XMLHttpRequest)
}
});
}

else if (uploadEventToMongoBlobFlag) {
    const packet = {
        eventTitle: $('#community-event-title').val(),
        eventDesc: $('#community-event-desc').val(),
        filePath: checkedCommunityEventRadio ? imageEventFileNamesToUploadToCommunity : [selectedEventVideoName],
        isVideo: checkedCommunityEventRadio ? false : true,
        creationDateAndTime: toIsoString(new Date()),
        eventDateStart: toIsoString(moment($('#community-event-start').val()).toDate()),
        eventDateEnd: toIsoString(moment($('#community-event-end').val()).toDate()),
        url: $('#community-event-url').val(),
        venue: $('#community-event-venue').val(),
        eventType: $('#community-event-type').val(),
        price: $('#community-event-price').val(),
        ipAddress: ipAddress,
        macAddress: macAddress,
        createdBy: userId,
        noOfLikes: [],
        comments: [],
    }
    $.ajax({
        url: `${communityURL}/addNewEvent/${selectedCommunityId}/${userId}`,
method: "PATCH",
    data: JSON.stringify(packet),
contentType: "application/json",
success: function (data) {
    uploadEventToCommunityBtn.innerHTML = `Post`;
    allEventVideoFiles = '';
    allEventImageFiles = '';
    communityEventForm.querySelector('.community-video-upload-preview').classList.remove('hide');
    communityEventForm.querySelector('.community-image-upload-preview').classList.remove('hide');
    communityEventForm.querySelectorAll('.image-event-preview-grid').forEach(div => div.innerHTML = '');
    communityEventVideoPlayer.classList.add('hide');
    checkedCommunityEventRadio = 'eventImageUpload';
    communityEventForm.reset();
    uploadEventToMongoBlobFlag = false;
    $('#communityEventUploadModal').modal('hide');
    Swal.fire(
        'Event Added!',
        'Your community administrators have been notified, once they approve your post it will be visible to all members in the community.',
        'success'
    );
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    uploadEventToMongoBlobFlag = false;
    console.error('error', XMLHttpRequest)
}
});

}


});

    const postFromUser = document.querySelector('#post-from-user');
postFromUser.addEventListener('click', () => {
    const input = postFromUser.parentElement.querySelector('input');
if (!input.value) {
    iziToast.error({ message: 'Post message cannot be empty!' });
    input.focus();
    return;
}
        const packet = {
            postDescription: input.value,
            filePath: [],
            creationDateAndTime: toIsoString(new Date()),
            ipAddress: ipAddress,
            macAddress: macAddress,
            createdBy: userId,
            noOfLikes: [],
            comments: []
        }
postFromUser.innerHTML = ` <i class="fa fa-circle-notch fa-spin font-18"></i>`;
$.ajax({
    url: `${communityURL}/addNewPost/${selectedCommunityId}/${userId}`,
    method: "PATCH",
data: JSON.stringify(packet),
contentType: "application/json",
success: function (data) {
    postFromUser.innerHTML = `<i class="bi bi-send-fill font-18 "></i>`;
    input.value = '';
    Swal.fire(
        'Post Added!',
        'Your community administrators have been notified, once they approve your post it will be visible to all members in the community.',
        'success'
    );
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    uploadImagesToMongoBlobFlag = false;
    console.error('error', XMLHttpRequest)
}
});
});

});

/**
 * Function to check if a user is present in the college community
 */
function checkIfANewMember() {
    const packet = {
        memberName: CurrentMemberName,
        memberImage: '',
        userId: userId,
    }
    console.log(`${communityMemberURL}/addANewMember/${memberContId}/${userId}`)
$.ajax({
    url: `${communityMemberURL}/addANewMember/${memberContId}/${userId}`,
    method: "PATCH",
data: JSON.stringify(packet),
contentType: "application/json",
success: function (data) {
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

/**
 * Get all the members of the specified organization
 */
function getAllMembersInCommunity() {
    console.log(`${communityMemberURL}/${orgId}/${collegeId}`)
    $.ajax({
        url: `${communityMemberURL}/${orgId}/${collegeId}`,
    method: "GET",
async: false,
contentType: "application/json",
success: function (data) {
    console.log('all members',data)
    communityMemberContainer.id = data._id;
    memberContId = data._id;
    allMembersOA = data.allMembers;
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

/**
 * Function to search all communities
 */
function makeAllCommunitiesModal() {
    const newElement = document.createElement('div');
    newElement.className = 'modal fade';
    newElement.id = "allCommunitiesModal";
    newElement.innerHTML = `
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">All Communities</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
                <!--search bar-->
                <div id="all-communities-container" class="row">
                    <code class="col-12 d-block text-center">No communities have been created yet!</code>
                </div>
            </div>            
        </div>
    </div>
    `;
    allCommunityBody = newElement.querySelector('#all-communities-container');
    document.body.appendChild(newElement);
}


/**
 * FUnction to make the display modal for news
 */
function makeDisplayModalForNews() {
    const newElement = document.createElement('div');
    newElement.className = 'modal fade';
    newElement.id = 'newsModal';
    newElement.innerHTML = `   
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">News</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
                <h6 id='community-modal-news-title'></h6> 
                <div id='community-modal-news-content' class="">                </div>
            </div>
        </div>
    </div>
    `;

    communityModalTitle = newElement.querySelector('#community-modal-news-title');
    communityNewsModalDiv = newElement.querySelector('#community-modal-news-content');

    document.body.appendChild(newElement);

}


/**
 * Function to fetch all news for a college
 */
function getAllNewsForCollege() {

    $.ajax({
        url: `${communityNewsURL}/${orgId}/${collegeId}`,
method: "GET",
    contentType: "application/json",
success: function (data) {
    console.log(`${communityNewsURL}/${orgId}/${collegeId}`,data)
newsContainer.id = data._id;
newsContainer.innerHTML = '';
if (data.allNews.length == 0) {
    newsContainer.innerHTML = ` <img src="../Images/Community/no_news_yet.jpg" class="img-fluid"  alt="No news available" style="margin:-16px;" defer></img>`
}
else {
    data.allNews.forEach(news => {
        populateNewsContent(news, newsContainer)
    });
}
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

/**
 * Function to make the news for a college
 */
function populateNewsContent(data, container) {
    const newElement = document.createElement('li');
    newElement.className = 'news-content';
    newElement.innerHTML = `
        <div class="d-flex pe-3 align-items-center justify-content-between">
            <a href="${data.isURL ? data.url : '#'}" ${data.isURL ? '' : `data-target="#newsModal" data-toggle="modal"`}"  target="_blank" class="d-block news-title">
    ${data.newsTitle}
    <div class="news-time font-11">
        <i class="bi bi-clock-history me-1"></i>
        <span>${timeSince(data.creationDateAndTime)}</span>
    </div>
</a>                          
<div class="news-options hide">
   <!-- <i class="bi font-16 bi-pencil-square tippy option-icon edit-icon " data-tippy-content="Edit News" data-news-id='${data._id}'></i>-->
    <i class="bi font-16 tippy bi-trash option-icon delete-icon " data-tippy-content="Delete News" data-news-id='${data._id}'></i>
</div>           
</div>
    `;
    const editNewsBtn = newElement.querySelector('.edit-icon');

    const deleteNewsBtn = newElement.querySelector('.delete-icon');

    deleteNewsBtn.addEventListener('click', () => {
        const deleteId = deleteNewsBtn.dataset.newsId;
    Swal.fire({
        title: `Delete "${data.newsTitle}"?`,
        text: "You won't be able to revert this!",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, delete it!'
}).then((result) => {
    if (result.isConfirmed) {
        $.ajax({
    url: `${communityNewsURL}/deleteNews/${newsContainer.id}/${deleteId}`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    getAllNewsForCollege();
    removeEditableNews();
    Swal.fire(
        'Deleted!',
        'Your News has been deleted.',
        'success'
    )
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});

}
});
});

tippy(editNewsBtn, {
    arrow: true,
    theme: 'otika',
});

tippy(deleteNewsBtn, {
    arrow: true,
    theme: 'Logout',
});


if (!data.isURL) {
    const anchor = newElement.querySelector('a.news-title');
    anchor.addEventListener('click', (e) => {
        e.stopPropagation();
    communityModalTitle.innerHTML = data.newsTitle;
    communityNewsModalDiv.innerHTML = data.newsDescription;
});
}

//if modal populate the modal content
container.prepend(newElement);
}

/**
 * Function to make a poll for community
 */
function makeCommunityPollModal() {
    const newElement = document.createElement('div');
    newElement.className = "modal fade";
    newElement.id = "communityPollUploadModal";
    newElement.innerHTML = `
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Add Poll</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body  pb-0">
                    <!--  -->
                    
                    <ul class="nav nav-tabs" id="myTab" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="community-new-poll-tab" data-toggle="tab" href="#community-new-poll" role="tab" aria-controls="community-new-poll" aria-selected="true">New Poll</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="community-my-polls-tab" data-toggle="tab" href="#community-my-polls" role="tab" aria-controls="community-my-polls" aria-selected="false">My Polls</a>
                        </li>
                    </ul>
                    <div class="tab-content mt-3" id="myTabContent">
                        <div class="tab-pane fade show active" id="community-new-poll" role="tabpanel" aria-labelledby="community-new-poll-tab">
                            <form> 
                                <div class="form-group row">
                                    <div class="col-lg-8 col-md-8 col-12">
                                        <label for="community-poll-title"><sup>*</sup>Poll Question</label>
                                        <input type="text" class="form-control" id="community-poll-title" placeholder="e.g. How long did it take for the support team to respond to you? "></input>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-12">
                                        <label for="community-poll-expire"><sup>*</sup>Poll Expiration</label>
                                        <input type="datetime-local" min="${moment().format('YYYY-MM-DDThh:mm')}" class="form-control" id="community-poll-expire"></input>
                                    </div>
                                </div>
                                <div class="form-group d-flex justify-content-between align-items-center">
                                    <label for="community-add-poll-option"><sup>*</sup>Options</label>
                                    <i class="bi bi-plus-square blue-icon text-success" id="community-add-poll-option"></i>                            
                                </div>
                                <div class="form-group" id="community-poll-option-container">
                                </div>
                                <div class="text-right form-group">
                                    <button type="button" class="btn btn-primary" id="upload-poll-community" value="Post" style="min-width:110px;"/>Post</button>
                                    <button type="button" class="btn btn-outline-primary" id="reset-community-poll-form">Reset</button>
                                    <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
                                </div>
                            </form>   
                        </div>
                        <div class="tab-pane fade" id="community-my-polls" role="tabpanel" aria-labelledby="community-my-polls-tab">
                            <img src="../Images/Community/no_polls_yet.jpg" class="img-fluid"  alt="No Posts Available" defer></img>
                        </div>                        
                    </div>
                   
                                 
                    <!--  -->
                </div>
            </div>
        </div>
    `;

    communityPollForm = newElement.querySelector('form');

    communityPollPostContent = newElement.querySelector('#community-user-poll-post-upload');

    communityPollOptionContainer = newElement.querySelector('#community-poll-option-container');

    communityMyPollsContainer = newElement.querySelector('#community-my-polls');

    const pollName = newElement.querySelector('#community-poll-title');

    const pollExpiration = newElement.querySelector('#community-poll-expire');

    uploadPostsToCommunityBtn = newElement.querySelector('#upload-poll-community');
     uploadPostsToCommunityBtn.addEventListener('click', () => {
        let savePoll = true;
        if (!pollName.value) {
            iziToast.error({ message: 'Poll name cannot be empty!' });
pollName.focus();
savePoll = false;
return;
}
if (!pollExpiration.value) {
    iziToast.error({ message: 'Poll expiration date cannot be empty!' });
    pollExpiration.focus();
    savePoll = false;
    return;
}
        const allPollOptions = newElement.querySelectorAll('.community-poll-option');
if (allPollOptions.length == 0) {
    iziToast.error({ message: 'Poll options cannot be empty!' });
    savePoll = false;
    return;
}
if (allPollOptions.length < 2) {
    iziToast.error({ message: 'Poll needs to have at least two options!' });
    savePoll = false;
    return;
}
if (allPollOptions.length == 2) {
    allPollOptions.forEach(poll => {
        if (!poll.value) {
            iziToast.error({ message: 'Poll options cannot be empty!' });
poll.focus();
savePoll = false;
return;
}
});
}
if(savePoll){
    const pollValues = [];
    allPollOptions.forEach(poll => {
        if (poll.value) {
            pollValues.push({
        option: poll.value,
    answeredBy: []
});
}
});
uploadPostsToCommunityBtn.innerHTML = `<i class="fa fa-circle-notch fa-spin font-18"></i>`;
uploadPollToMongoBlob(pollName.value, pollValues, pollExpiration.value);
}
        
});

    const addNewPoll = newElement.querySelector('#community-add-poll-option');
addNewPoll.addEventListener('click', addNewCommunityPollOption);

communityPollContainer = newElement.querySelector('#community-poll-option-container');

addNewCommunityPollOption();
addNewCommunityPollOption();

    const resetForm = newElement.querySelector('#reset-community-poll-form');
resetForm.addEventListener('click', () => {
    communityPollOptionContainer.innerHTML = ``;
addNewCommunityPollOption();
addNewCommunityPollOption();
communityPollForm.reset();
});

document.body.appendChild(newElement);
}

/**
 * Function to upload poll to mongo db
 */
function uploadPollToMongoBlob(pollName, pollOptions, pollExpire) {
    const packet = {
        pollTitle: pollName,
        pollOptions,
            ipAddress: ipAddress,
    macAddress: macAddress,
    createdBy: userId,
    creationDateAndTime: toIsoString(new Date()),
    validTill: toIsoString(moment(pollExpire).toDate())
}
$.ajax({
    url: `${communityURL}/addNewPoll/${selectedCommunityId}/${userId}`,
    method: "PATCH",
data: JSON.stringify(packet),
contentType: "application/json",
success: function () {
    uploadPostsToCommunityBtn.innerHTML = `Post`;
    communityPollOptionContainer.innerHTML = ``;
    addNewCommunityPollOption();
    addNewCommunityPollOption();
    communityPollForm.reset();
    $('#communityPollUploadModal').modal('hide');
    Swal.fire(
        'Poll Added!',
        'Your community administrators have been notified, once they approve your poll it will be visible to all members in the community.',
        'success'
    );
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

/**
 * Add new community poll
 */
function addNewCommunityPollOption() {
    const newElement = document.createElement('div');
    newElement.className = "form-group d-flex justify-content-between align-items-center";
    newElement.innerHTML = `
        <div class="w-100">            
            <input type="text" class="form-control community-poll-option"  placeholder="e.g. 0 - 10 mins ">
        </div>
        <i class="bi bi-trash deleteIcon font-18 text-danger"></i>
    `;
    const deleteIcon = newElement.querySelector('.deleteIcon');
    deleteIcon.addEventListener('click', () => {
        newElement.remove();
});

communityPollContainer.appendChild(newElement);
}

/**
 * Modal to make the community event upload
 */
function makeCommunityEventModal() {
    const newElement = document.createElement('div');
    newElement.className = "modal fade";
    newElement.id = "communityEventUploadModal";
    newElement.innerHTML = `
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Add Event</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body pb-0">
                <form> 
                    <div class="form-group">
                        <label for="community-event-title"><sup>*</sup>Title</label>
                        <input type="text" class="form-control" id="community-event-title" placeholder="e.g. Computing Networking Excellence "></input>
                    </div>
                    <div class="form-group">
                        <label for="community-event-desc"><sup>*</sup>Description</label>
                        <textarea type="text" class="form-control" id="community-event-desc" placeholder="e.g. A workshop for computer networking enthusiasts where you can hone your skills..."></textarea>
                    </div>
                    <div class="form-group">
                        <label for="community-event-url">URL</label>
                        <input type="text" class="form-control" id="community-event-url" placeholder="e.g. https://www.website.com/event"></input>
                    </div>
                    <div class="form-group">
                        <label for="community-event-venue">Venue</label>
                        <input type="text" class="form-control" id="community-event-venue" placeholder="e.g. State Side Stadium"></input>
                    </div>
                    <div class="form-group">
                        <div class="row"> 
                            <div class="col-lg-6 vol-md-6 col-12">
                                <label for="community-event-start">Start Time</label>
                                <input type="datetime-local" min="${moment().format('YYYY-MM-DDThh:mm')}" class="form-control" value="0" id="community-event-start" ></input>
                            </div>
                            <div class="col-lg-6 vol-md-6 col-12">
                                <label for="community-event-end">End Time</label>
                                <input type="datetime-local" min="${moment().format('YYYY-MM-DDThh:mm')}" class="form-control" value="0" id="community-event-end" placeholder="e.g. State Side Stadium"></input>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row"> 
                            <div class="col-lg-6 vol-md-6 col-12">
                                <label for="community-event-type"><sup>*</sup>Type</label>
                                <select type="text" class="form-control" id="community-event-type" data-select2-enable="true">
                                    <option value="-1">Please Select</option>
                                    <option value="1">Online</option>
                                    <option value="2">Offline</option>
                                </select>
                            </div>
                            <div class="col-lg-6 vol-md-6 col-12">
                                <label for="community-event-price">Price <code>(In Rupees)</code></label>
                                <input type="number" class="form-control" value="0" id="community-event-price" placeholder="e.g. Rs. 250.00" ></input>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="d-block">Upload</label>
                        <div class="form-check form-check-inline d-none">
                            <input class="form-check-input" name="eventUpload" type="radio" id="eventImageUpload" value="option1" checked>
                            <label class="form-check-label" for="eventImageUpload">Image</label>
                        </div>
                        <div class="form-check form-check-inline d-none">
                            <input class="form-check-input" name="eventUpload" type="radio" id="eventVideoUpload" value="option2">
                            <label class="form-check-label" for="eventVideoUpload">Video</label>
                        </div>
                    </div>
                    <div class="community-event-image-container">
                        <div class="text-center preview-selected-image mb-3" id="previewCommunityEventImages">
                            <h6 class="text-secondary community-image-upload-preview" style="grid-column:span 5;">Image Preview</h6>
                            <div class="image-event-preview-grid image-event-preview-grid-1"></div>
                            <div class="image-event-preview-grid image-event-preview-grid-2"></div>
                            <div class="image-event-preview-grid image-event-preview-grid-3"></div>
                            <div class="image-event-preview-grid image-event-preview-grid-4"></div>
                        </div>
                        <div class="form-group">
                            <input type="file" id="community-user-event-image-upload" class="form-control" name="file" accept="image/*" multiple="">
                        </div>
                    </div>
                    <div class="community-event-video-container hide">
                        <div class="text-center preview-selected-image mb-3" id="previewCommunityEventVideo">
                            <h6 class="text-secondary community-video-upload-preview" style="grid-column:span 5;">Video Preview</h6>
                            <video class="w-100 hide" controls 
                                    onloadedmetadata="this.muted = true"
                                    id="community-event-video-preview"
                                    onmouseenter="play()"
                                    onmouseleave="pause()"
                                    style="grid-column:span 5;">
                                Your browser does not support the video tag.
                            </video>
                        </div>
                        <div class="form-group">
                            <input type="file" id="community-user-event-video-upload" class="form-control" name="file" accept="video/mp4,video/x-m4v,video/*">
                        </div>
                    </div>
                    <div class="text-right form-group">
                        <button type="button" class="btn btn-primary" id="upload-event-community" value="Post" style="min-width:110px;">Post</button>
                        <button type="button" class="btn btn-outline-primary" id="reset-community-event-form">Reset</button>
                        <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
                    </div>
                </form>
            </div>            
        </div>
    </div>
    `;

    communityEventForm = newElement.querySelector('form');

    communityEventVideoPostContent = newElement.querySelector('#community-user-event-video-upload');

    const eventRadioBtns = newElement.querySelectorAll('[name="eventUpload"]');
    eventRadioBtns.forEach(rb => {
        const imageContainer = newElement.querySelector('.community-event-image-container');
        const videoContainer = newElement.querySelector('.community-event-video-container');
        rb.addEventListener('change', (e) => {
            if (rb.id == "eventImageUpload") {
                imageContainer.classList.remove('hide');
                videoContainer.classList.add('hide');
                        }
else {
                imageContainer.classList.add('hide');
videoContainer.classList.remove('hide');
checkedCommunityEventRadio = '';
}
});
});

    const uploadBtn = newElement.querySelector('#upload-event-community');
uploadBtn.addEventListener('click', () => {
    if (!newElement.querySelector('#community-event-title').value) {
        iziToast.error({ message: 'Event name cannot be empty!' });
newElement.querySelector('#community-event-title').focus();
return;
}
if (!newElement.querySelector('#community-event-desc').value) {
    iziToast.error({ message: 'Event description cannot be empty!' });
    newElement.querySelector('#community-event-desc').focus();
    return;
}
if (newElement.querySelector('#community-event-type').value == -1) {
    iziToast.error({ message: 'Event type cannot be empty!' });
    newElement.querySelector('#community-event-type').focus();
    return;
}
console.log('test')
checkedCommunityEventRadio ? uploadEventImagesToMongoBlob() : uploadEventVideoToMongoBlob();
});

    const resetForm = newElement.querySelector('#reset-community-event-form');
resetForm.addEventListener('click', () => {
    allEventVideoFiles = '';
allEventImageFiles = '';
checkedCommunityEventRadio = 'eventImageUpload';
communityEventForm.querySelector('.community-video-upload-preview').classList.remove('hide');
communityEventForm.querySelector('.community-image-upload-preview').classList.remove('hide');
communityEventForm.querySelectorAll('.image-event-preview-grid').forEach(div => div.innerHTML = '');
communityEventVideoPlayer.classList.add('hide');
communityEventForm.reset();
});

document.body.appendChild(newElement);
}

/**
 * Modal to make the community video upload
 */
function makeCommunityVideoModal() {
    const newElement = document.createElement('div');
    newElement.className = "modal fade";
    newElement.id = "communityVideoUploadModal";
    newElement.innerHTML = `
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Add Video</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body pb-0">
                <!--  -->
                <form> 
                    <div class="text-center preview-selected-image mb-3" id="previewCommunityVideo">
                        <h6 class="text-secondary community-video-upload-preview" style="grid-column:span 5;">Video Preview</h6>                        
                        <video class="w-100 hide" controls id="community-video-preview" style="grid-column:span 5;">
                            Your browser does not support the video tag.
                        </video>
                    </div>
                    <div class="form-group">
                        <input type="file" id="community-user-video-upload" class="form-control"  name="file" accept="video/mp4,video/x-m4v,video/*"/>
                    </div>
                    <div class="form-group">
                        <textarea class="form-control" id="community-user-video-post-upload" placeholder="Want to add something with this video?"></textarea>
                    </div>
                    <div class="text-right form-group">
                        <button type="button" class="btn btn-primary" id="upload-video-community" value="Post" style="min-width:110px;"/>Post</button>
                        <button type="button" class="btn btn-outline-primary" id="reset-community-video-form">Reset</button>
                        <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
                    </div>
                </form>                
                <!--  -->
            </div>            
        </div>
    </div>
    `;
    communityVideoForm = newElement.querySelector('form');

    communityVideoPostContent = newElement.querySelector('#community-user-video-post-upload');

    const uploadBtn = newElement.querySelector('#upload-video-community');
    uploadBtn.addEventListener('click', uploadVideoToMongoBlob);

    const resetForm = newElement.querySelector('#reset-community-video-form');
    resetForm.addEventListener('click', () => {
        allVideoFiles = '';
    communityVideoForm.querySelector('.community-video-upload-preview').classList.remove('hide');
    communityVideoPlayer.classList.add('hide');
    communityVideoForm.reset();
});

document.body.appendChild(newElement);
}

/**
 * Modal to make the community image upload
 */
function makeCommunityImageModal() {
    const newElement = document.createElement('div');
    newElement.classList.add('modal', 'fade');
    newElement.id = 'communityImageUploadModal'
    newElement.innerHTML = `
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Add Image(s)</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body pb-0">
                <!--  -->               
                <form> 
                    <div class="text-center preview-selected-image mb-3" id="previewCommunityImages">
                        <h6 class="text-secondary community-image-upload-preview" style="grid-column:span 5;">Image Preview</h6>
                        <div class="image-preview-grid image-preview-grid-1"></div>
                        <div class="image-preview-grid image-preview-grid-2"></div>
                        <div class="image-preview-grid image-preview-grid-3"></div>
                        <div class="image-preview-grid image-preview-grid-4"></div>
                    </div>
                    <div class="form-group">
                        <input type="file" id="community-user-image-upload" class="form-control" name="file" accept="image/*" multiple/>
                    </div>
                    <div class="form-group">
                        <textarea class="form-control" id="community-user-video-post-upload" placeholder="Want to add something with this image(s)?"></textarea>
                    </div>
                    <div class="text-right form-group">
                        <button type="button" class="btn btn-primary" id="upload-files-community" value="Post" style="min-width:110px;"/>Post</button>
                        <button type="button" class="btn btn-outline-primary" id="reset-community-image-form">Reset</button>
                        <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
                    </div>
                </form>
                <!--  -->
            </div>            
        </div>
    </div>
    `;

    communityImageForm = newElement.querySelector('form');

    communityImagePostContent = newElement.querySelector('#community-user-video-post-upload');

    const uploadBtn = newElement.querySelector('#upload-files-community');
    uploadBtn.addEventListener('click', uploadImagesToMongoBlob);

    const resetForm = newElement.querySelector('#reset-community-image-form');
    resetForm.addEventListener('click', () => {
        allImageFiles = '';
    communityImageForm.querySelector('.community-image-upload-preview').classList.remove('hide');
    communityImageForm.querySelectorAll('.image-preview-grid').forEach(div => div.innerHTML = '');
    communityPreviewContainerCount = 1;
    imageIndexForUploadInCommunity = 0;
    communityImageForm.reset();
});

document.body.appendChild(newElement);
}

/**
 * Function to get and populate all communityName
 */
function getAllCommunityNames() {
    $.ajax({
        url: `${communityURL}/${orgId}/${collegeId}/${userId}`,
method: "GET",
    contentType: "application/json",
success: function (data) {
    communityContainer.innerHTML = '';
    data.sort((a, b) => b.isGeneral - a.isGeneral).forEach((d, i) => {
        if (d.incharge.filter(obj => obj.inchargeId == userId).length != 0) {
            inchargeFor.push(d.id);
}
if (i == 0)
    populateCommunityList(d, d._id, i);
else if (d.members.filter(obj => obj.memberId == userId && obj.isVerified).length > 0) {
    populateCommunityList(d, d._id, i);
}
else {
    populateAllCommunityList(d);
}
});
document.querySelector('[data-community-general="true"]').click();
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

/**
 * All community list 
 */
function populateAllCommunityList(data) {
    const newElement = document.createElement('div');
    newElement.className = 'col-lg-6 col-md-6 col-12 ';
    newElement.innerHTML = `
    <div class="post-type py-2 d-flex justify-content-between align-items-center">
        <div class="d-flex g-1 align-items-center ">
            <i class="todo-icons bi  ${data.communityIcon}" style="color:white;background-color:${data.communityIconColor}"></i>
            <div class="community-name ml-2 font-16">${data.communityName}</div>
        </div>
        <div class="">
            <span class="badge badge-success font-13">${data.members.length} <i class="bi bi-people-fill"></i></span>
            <button type="button" class="btn btn-primary btn-sm joinCommunityBtn" data-community-join-id="${data.id}">+ Join</button>
        </div>
    </div>
    `;



    const joinBtn = newElement.querySelector('.joinCommunityBtn');
    joinBtn.addEventListener('click', () => {
        const id = joinBtn.dataset.communityJoinId;
        const packet = {
            memberId: userId
        }
    Swal.fire({
        title: `Join "${data.communityName}"?`,
        text: "You can only see the messages once the community maintainers have approved you!",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, join it!'
}).then((result) => {
    if (result.isConfirmed) {
        $.ajax({
    url: `${communityURL}/addACommunityMember/${id}`,
method: "PATCH",
    data: JSON.stringify(packet),
contentType: "application/json",
success: function (data) {
    Swal.fire(
        'Request Sent!',
        'The community maintainers have been notified about your request.',
        'success'
    )
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});

}
});
});

allCommunityBody.appendChild(newElement);

}

/**
 * Function to upload images to mongo 
 */
function uploadImagesToMongoBlob() {
    if(allImageFiles.length == 0){
        iziToast.error({message: 'Image Cannot be Empty'});
        return;
    }
    for (i = 0; i < allImageFiles.length; i++) {
        selectedCommunityImage = allImageFiles[i];
        fileName = `${toIsoString(new Date())}_${userId}_${Math.random().toString(32).slice(2)}_${selectedCommunityImage.name}`;
        fileSize = selectedCommunityImage.size;
        fileType = selectedCommunityImage.type;
        var fileContent = selectedCommunityImage.slice(currentFilePointer, currentFilePointer + maxBlockSize);
        var reader = new FileReader();
        reader.readAsArrayBuffer(fileContent);
        sendToMongoBlob(reader, fileName, fileType, 'image');
        imageFileNamesToUploadToCommunity.push(fileName);
    };
    uploadImagesToMongoBlobFlag = true;
}

/**
 * Function to upload event videos to mongo 
 */
function uploadEventImagesToMongoBlob() {
    if(allEventImageFiles.length == 0){
        iziToast.error({message: 'Image Cannot be Empty'});
        return;
    }
    for (i = 0; i < allEventImageFiles.length; i++) {
        selectedCommunityEventImage = allEventImageFiles[i];
        fileName = `${toIsoString(new Date())}_${userId}_${Math.random().toString(32).slice(2)}_${selectedCommunityEventImage.name}`;
        fileSize = selectedCommunityEventImage.size;
        fileType = selectedCommunityEventImage.type;
        var fileContent = selectedCommunityEventImage.slice(currentFilePointer, currentFilePointer + maxBlockSize);
        var reader = new FileReader();
        reader.readAsArrayBuffer(fileContent);
        sendToMongoBlob(reader, fileName, fileType, 'event');
        imageEventFileNamesToUploadToCommunity.push(fileName);
    };
    uploadEventToMongoBlobFlag = true;
}


/**
 * Function to upload video to mongo 
 */
function uploadVideoToMongoBlob() {
    selectedCommunityVideo = allVideoFiles[0];
    fileName = `${toIsoString(new Date())}_${userId}_${Math.random().toString(32).slice(2)}_${selectedCommunityVideo.name}`;
    fileSize = selectedCommunityVideo.size;
    fileType = selectedCommunityVideo.type;
    var fileContent = selectedCommunityVideo.slice(currentFilePointer, currentFilePointer + maxBlockSize);
    var reader = new FileReader();
    reader.readAsArrayBuffer(fileContent);
    sendToMongoBlob(reader, fileName, fileType, 'video');
    selectedVideoName = fileName;
    uploadVideosToMongoBlobFlag = true;
}

/**
 * Function to upload event video to mongo 
 */
function uploadEventVideoToMongoBlob() {
    selectedCommunityEventVideo = allEventVideoFiles[0];
    fileName = `${toIsoString(new Date())}_${userId}_${Math.random().toString(32).slice(2)}_${selectedCommunityEventVideo.name}`;
    fileSize = selectedCommunityEventVideo.size;
    fileType = selectedCommunityEventVideo.type;
    var fileContent = selectedCommunityEventVideo.slice(currentFilePointer, currentFilePointer + maxBlockSize);
    var reader = new FileReader();
    reader.readAsArrayBuffer(fileContent);
    sendToMongoBlob(reader, fileName, fileType, 'event');
    selectedEventVideoName = fileName;
    uploadEventToMongoBlobFlag = true;
}

/**
 * Function to upload files to azure blob 
 */
function sendToMongoBlob(reader, fileName, fileType, type) {
    if (type == 'image')
        uploadFilesToCommunityBtn.innerHTML = `
            <i class="fa fa-circle-notch fa-spin font-18"></i>
        `;
else if (type == 'video')
    uploadVideosToCommunityBtn.innerHTML = `
        <i class="fa fa-circle-notch fa-spin font-18"></i>
        `;
else if (type == 'event')
    uploadEventToCommunityBtn.innerHTML = `
        <i class="fa fa-circle-notch fa-spin font-18"></i>
        `;

    reader.onloadend = function (evt) {
        var requestData = new Uint8Array(evt.target.result);
        fileItem = requestData;
        $.ajax({
            url: `${communityBlobContainer}/${orgId}/${collegeId}/${fileName}?si=communitydoc-policy&spr=https&sv=2021-12-02&sr=c&sig=GvLoNgqLRwMB9mVosyU3Vwpv3O7BXk0Wx0103M%2F%2BwIU%3D`,
    type: "PUT",
    data: fileItem,
    contentType: `${fileType}`,
    processData: false,
    headers: {
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS",
        "Access-Control-Allow-Headers": "Content-Type, Authorization, Content-Length, X-Requested-With",
        "Access-Control-Allow-Credentials": true
    },
    beforeSend: function (xhr) {
        xhr.setRequestHeader('x-ms-blob-type', 'BlockBlob');
    },
    success: function (data, status) {
        console.log(status)
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.error('error', XMLHttpRequest)
    }
});
};
}

/**
 * Toggle Message
 */
function toggleCommunityMessage(element) {
    const messagesContainer = element.closest('.card').querySelector('.post-others-messages');
    if (messagesContainer.classList.contains('hide')) {
        messagesContainer.classList.remove('hide');
        element.classList.add('blue-icon');
    }
    // else{
    //     messagesContainer.classList.add('hide');        
    //     element.classList.remove('blue-icon');
    // }
}

/**
 * make the news modal and initialize it
 */
function makeModalToAddNews() {
    const newElement = document.createElement('div');
    newElement.classList.add('modal', 'fade');
    newElement.id = "addMoreNewsModal";
    newElement.innerHTML = `
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Add News</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <label><sup>*</sup>Title</label>
                                    <input type="text" class="form-control" id="community-news-title" placeholder="e.g. Rajasthan NEET UG Counselling 2022">
                                </div>
                            </div>
                            <div class="form-group community-element col-12">
                                <label class="d-block"><sup>*</sup>News Type</label>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" name="community-news-radio" type="radio" id="newsURLContainer" value="option1" checked>
                                    <label class="form-check-label" for="newsURLContainer">URL</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" name="community-news-radio" type="radio" id="newsPostContainer" value="option2">
                                    <label class="form-check-label" for="newsPostContainer">Article</label>
                                </div>
                            </div>
                            <div class="form-group col-12">
                                <div class="newsURLContainer">
                                    <label><sup>*</sup>News URL</label>
                                    <input type="text" class="form-control" name="" id="community-news-url" placeholder="e.g. https://www.bbc.com/news/article"></input>
                                </div>

                                <div class="newsPostContainer hide">
                                    <div class="form-group mb-0">
                                        <label><sup>*</sup>Short Description<code></code></label>
                                        <textarea class="form-control community-summernote summernote" id="summernote"></textarea>
                                    </div>                                                             
                                </div>
                            </div>
                            
                            <div class="col-12 btn-footer">
                                <button type="button" class="btn btn-primary" id="add-community-news" style="min-width:70px;">Create</button>
                                <button type="button" class="btn btn-outline-primary" id="reset-community-news">Reset</button>
                                <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    `;

    const form = newElement.querySelector('form');

    const newsPostContainer = newElement.querySelector('.newsPostContainer');

    const newsURLContainer = newElement.querySelector('.newsURLContainer');

    let selectedRb = 'newsURLContainer';
    const newsRbs = newElement.querySelectorAll('[name="community-news-radio"]');
    newsRbs.forEach(rb => {
        rb.addEventListener('change', () => {
            if (rb.id == 'newsURLContainer') {
                newsURLContainer.classList.remove('hide');
                newsPostContainer.classList.add('hide');
                selectedRb = 'newsURLContainer';
                                    }
else {
                newsURLContainer.classList.add('hide');
newsPostContainer.classList.remove('hide');
selectedRb = 'newsPostContainer';
}
});
});

    const resetBtn = newElement.querySelector('#reset-community-news');
resetBtn.addEventListener('click', () => {
    $("#summernote").summernote('code', '');
newsURLContainer.classList.remove('hide');
newsPostContainer.classList.add('hide');
form.reset();
});

    const newsTitle = newElement.querySelector('#community-news-title');

    const url = newElement.querySelector('#community-news-url');

    const uploadBtn = newElement.querySelector('#add-community-news');
uploadBtn.addEventListener('click', () => {

    if (!newsTitle.value) {
        iziToast.error({ message: 'News title cannot be empty!' });
newsTitle.focus();
return;
}
let packet;
console.log(selectedRb);
if (selectedRb == 'newsURLContainer') {
    if (!url.value) {
        iziToast.error({ message: 'URL cannot be empty!' });
        url.focus();
        return;
    }
    packet = {
        newsTitle: newsTitle.value,
        creationDateAndTime: toIsoString(new Date()),
        url: url.value,
        isURL: true,
        createdBy: userId,
        ipAddress: ipAddress,
        macAddress: macAddress,
    }
}
else {
    if (!$("#summernote").summernote('code')) {
        iziToast.error({ message: 'Description cannot be empty!' });
        $("#summernote").focus();
        return;
    }
    packet = {
        newsTitle: newsTitle.value,
        creationDateAndTime: toIsoString(new Date()),
        newsDescription: $("#summernote").summernote('code'),
        createdBy: userId,
        ipAddress: ipAddress,
        macAddress: macAddress,
    }
}
uploadBtn.innerHTML = `<i class="fa fa-circle-notch fa-spin font-18"></i>`;
        const newsFor = newsContainer.id;
$.ajax({
    url: `${communityNewsURL}/addNewNews/${newsFor}`,
    method: "PATCH",
data: JSON.stringify(packet),
contentType: "application/json",
success: function (data) {
    uploadBtn.innerHTML = 'Create';
    $("#summernote").summernote('code', '');
    newsURLContainer.classList.remove('hide');
    newsPostContainer.classList.add('hide');
    form.reset();
    getAllNewsForCollege();
    Swal.fire(
        'News Added!',
        '',
        'success'
    );
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
});

document.body.appendChild(newElement);

$('#addMoreNewsModal').on('shown.bs.modal', function () {
    $("#summernote").summernote({
        dialogsInBody: true,
        minHeight: 250
    });
    $('.dropdown-toggle').dropdown();
});

}

/**
 * Make the community modal and initialize it 
 */
function makeModalForAddingCommunity() {
    const newElement = document.createElement('div');
    newElement.classList.add('modal', 'fade');
    newElement.id = "addMoreCommunityModal";
    newElement.innerHTML = ` 
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Community Properties</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body pb-0">
                <div class="text-center">
                    <i class="bi bi-list-task dot-red" data-icon-color="#F3565D" data-icon-type="bi-list-task" id="newCommunityIcon"></i>
                    <input type="text" class="form-control text-center mt-3" name="" id="communityName" placeholder="Communinty Name">
                </div>
                <hr>
                <div class="form-group">
                    <div class="selectgroup w-100 flex-wrap">
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#F3565D" class="selectgroup-input-radio" checked="">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-red"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#fe9701" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-orange"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#00bcd4" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-blue"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#1bbc9b" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-green"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#DC35A9" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-pink"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#9b59b6" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-purple"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#6777EF" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-dark-blue"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#78938A" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-dark-green"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#6190AF" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-emerald"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#878B99" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-gray"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#CC704B" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-dark-orange"></output></span>
                    </label>
                    <label class="selectgroup-item">
                        <input type="radio" name="community-color-group" value="#fb6f92" class="selectgroup-input-radio">
                        <span class="selectgroup-button d-flex align-items-center justify-content-center"><output class="dot dot-light-pink"></output></span>
                    </label>
                    </div>
                </div>  
                <hr>
                <div class="form-group">
                    <div class="selectgroup w-100 custom-icon-selectgroup" id="community-icon-select-group">

                    </div>
                </div> 
                <hr>
                <div class="row align-items-center">
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="form-group">
                        <label class=""><sup>*</sup>Community Maintainer</label>
                        </div>
                    </div>                    
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="form-group dropdownParent">
                            <select class="col-lg-6 col-md-6 col-12 form-control form-select multiselect-select2" id="ddCommunityIncharge" multiple>
                                <option value="-1" disabled>Please Select</option>
                                <option value="0">Person 1</option>
                                <option value="1">Person 2</option>
                                <option value="2">Person 3</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer pt-0">
                <button type="button" class="btn btn-primary" id="editCommunityProperties">Save changes</button>
                <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>            
            </div>
        </div>
    </div>
    `;


    $.ajax({
        url: '/Comman/GetDropDownUser/',
        dataType: "json",
        type: 'GET',
        success: function (data) {

            RenderDropDown($('#ddCommunityIncharge'), data['data'])
        }
    });

    newCommunityIcon = newElement.querySelector('#newCommunityIcon');

    communityName = newElement.querySelector('#communityName');

    editCommunityProperties = newElement.querySelector('#editCommunityProperties');
    editCommunityProperties.addEventListener('click', makeOrEditCommunity);

    communityIncahrgeDD = newElement.querySelector('#ddCommunityIncharge');


    communityIconGroup = newElement.querySelector('#community-icon-select-group');
    populateAllCommunityIcons();

    allCommunityListColors = newElement.querySelectorAll('[name="community-color-group"]');
    allCommunityListColors.forEach(option => option.addEventListener('change', toggleCommunityIconColors));

    allCommunityListIcons = newElement.querySelectorAll('[name="community-icon-group"]');
    allCommunityListIcons.forEach(option => option.addEventListener('change', toggleCommunityListIconContent));

    document.body.appendChild(newElement);

    $(communityIncahrgeDD).select2({
        dropdownParent: $('#addMoreCommunityModal .dropdownParent')
    });
}

/**
 * function to make or edit the community
 */
function makeOrEditCommunity() {
    if (!communityName.value) {
        iziToast.error({ message: 'Community name cannot be empty!' });
        communityName.focus();
        return;
    }
    const editIconId = editCommunityProperties.dataset.editCommunityId;
    if (editIconId == '') {
        if (allCommunityNames.includes(communityName.value.toLowerCase())) {
            iziToast.error({ message: 'A community with this name already exists!' });
            communityName.focus();
            return;
        }
        const allIncharge = $(communityIncahrgeDD).val();
        allIncharge.push(userId);
        if (allIncharge.length == 0) {
            iziToast.error({ message: 'Community needs to have at least one person incharge!' });
            communityIncahrgeDD.focus();
            return;
        }
        const inchargeList = allIncharge.map(val => { return { inchargeId: val } });
        const members = allIncharge.map(val => { return { memberId: val, isVerified: true } })
        const newCommunity = {
            'communityName': communityName.value,
            'communityIcon': newCommunityIcon.dataset.iconType,
            'communityIconColor': newCommunityIcon.dataset.iconColor,
            "creationDateAndTime": toIsoString(new Date()),
            "collegeId": collegeId,
            "orgId": orgId,
            "incharge": inchargeList,
            "createdBy": userId,
            "allPosts": [],
            "members": members
        }
        const packet = newCommunity;
        $.ajax({
            url: `${communityURL}/`,
    method: "POST",
    data: JSON.stringify(packet),
    contentType: "application/json",
    success: function (data) {
        populateCommunityList(newCommunity, data.id);
        $('#addMoreCommunityModal').modal('hide');
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.error('error', XMLHttpRequest)
    }
});
}
else {
    const allIncharge = $(communityIncahrgeDD).val();
    const inchargeList = allIncharge.map(val => { return { inchargeId: val } });
    const packet = {
        'communityName': communityName.value,
        'communityIcon': newCommunityIcon.dataset.iconType,
        'communityIconColor': newCommunityIcon.dataset.iconColor,
        "creationDateAndTime": toIsoString(new Date()),
        "incharge": inchargeList,
        "createdBy": userId,
    }
    $.ajax({
        url: `${communityURL}/editCommunityItem/${editIconId}`,
method: "PATCH",
    data: JSON.stringify(packet),
contentType: "application/json",
success: function (data) {
    getAllCommunityNames();
    removeEditableCommunity();
    $('#addMoreCommunityModal').modal('hide');
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
}

/**
 * function to populate the each new community
 */
function populateCommunityList(newCommunity, id = '', index) {
    allCommunityNames.push(newCommunity.communityName.toLowerCase());
    const newElement = document.createElement('div');
    newElement.className = 'community-element pl-3 pt-2 pr-3 pb-2 d-flex align-items-center justify-content-between border-bottom';
    index == 0 ? newElement.classList.add('general-community') : '';
    index == 0 ? newElement.dataset.communityGeneral = 'true' : '';
    const mainId = id ? id : newCommunity.id;
    newElement.dataset.communityId = mainId;
    /**
     * TODO: Check for notifications here
     */
    newElement.innerHTML = `
        <div class="d-flex g-1 align-items-center ">
            <i class="todo-icons bi ${newCommunity.communityIcon}" style="color:white;background-color:${newCommunity.communityIconColor}"></i>
            <div class="community-name ml-2 font-16">${newCommunity.communityName}</div>
        </div>
        <div class="d-flex align-items-center">
            <i class="bi font-16 bi-pencil-square tippy edit-icon hide" data-tippy-content="Edit Community"></i>
            <i class="bi font-16 bi-trash tippy ml-2 mr-2 delete-icon hide" data-tippy-content="Delete Community" data-delete-community-id="${mainId}"></i>
            <!--<div class="community-count rounded-circle">0</div>-->
        </div>    
    `;

    const editIcon = newElement.querySelector('.edit-icon');
    editIcon.addEventListener('click', () => {
        editCommunityProperties.dataset.editCommunityId = mainId;
    communityName.value = newCommunity.communityName;
    newCommunityIcon.dataset.iconType = newCommunity.communityIcon;
    newCommunityIcon.classList = `bi ${newCommunity.communityIcon} `;
    newCommunityIcon.classList.contains('dot-red') ? newCommunityIcon.classList.remove('dot-red') : '';
    newCommunityIcon.dataset.iconColor = newCommunity.communityIconColor;
    newCommunityIcon.style.backgroundColor = newCommunity.communityIconColor;
    newCommunityIcon.style.color = 'white';
    allCommunityListColors.forEach(colorRadio => {
        if (colorRadio.value == newCommunity.communityIconColor) {
            colorRadio.checked = true;
}
});
allCommunityListIcons.forEach(iconRadio => {
    if (iconRadio.value == newCommunity.communityIcon) {
        iconRadio.checked = true;
}
});
        const allIncharge = newCommunity.incharge.map(val => val.inchargeId);
$(communityIncahrgeDD).val(allIncharge).trigger('change');
$('#addMoreCommunityModal').modal('show');
});

    const deleteIcon = newElement.querySelector('.delete-icon');
deleteIcon.addEventListener('click', (e) => {
    e.stopPropagation();
        const deleteId = deleteIcon.dataset.deleteCommunityId;
Swal.fire({
    title: `Delete "${newCommunity.communityName}"?`,
    text: "You won't be able to revert this!",
icon: 'warning',
showCancelButton: true,
confirmButtonColor: '#3085d6',
cancelButtonColor: '#d33',
confirmButtonText: 'Yes, delete it!'
}).then((result) => {
    if (result.isConfirmed) {
        $.ajax({
    url: `${communityURL}/removeEntireCommunity/${deleteId}`,
method: "DELETE",
    contentType: "application/json",
success: function (data) {
    getAllCommunityNames();
    removeEditableCommunity();
    Swal.fire(
        'Deleted!',
        'Your Community has been deleted.',
        'success'
    )
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});

}
});
});

tippy(editIcon, {
    arrow: true,
    theme: 'otika',
});

tippy(deleteIcon, {
    arrow: true,
    theme: 'Logout',
});

if (index == 0) {
    editIcon.remove();
    deleteIcon.remove();
}



newElement.addEventListener("click", (e) => {
    const allCommunities = communityContainer.querySelectorAll('.community-element');
// console.log(allCommunities)
e.stopPropagation();
document.querySelector('.admin-options') ? document.querySelector('.admin-options').remove() : '';
postsContainer.innerHTML = ``;
allCommunities.forEach(comm => comm.classList.remove('active'));
newElement.classList.add("active");
//selected community
allMembersIC = newCommunity.members;
        const verifiedMembers = allMembersIC.filter(obj => obj.isVerified).map(obj => obj.memberId);
communityMemberContainer.innerHTML = '';
index == 0 ? document.querySelector('#community-member-count span').textContent = allMembersOA.length : document.querySelector('#community-member-count span').textContent = allMembersOA.filter(obj => verifiedMembers.includes(obj.userId)).length;
index == 0 ? makeMembersInACommunity(allMembersOA, communityMemberContainer) : makeMembersInACommunity(allMembersOA.filter(obj => verifiedMembers.includes(obj.userId)), communityMemberContainer);
localStorage.setItem('sc', mainId);
communityDisplayName.textContent = newCommunity.communityName;
communityDisplayName.dataset.selectedCommunityId = mainId;
postsContainer = document.querySelector('.posts-container');
if (document.querySelector('#communityUnverifiedPostsModal'))
    document.querySelector('#communityUnverifiedPostsModal').remove();
if (document.querySelector('#communityFlaggedCommentsModal'))
    document.querySelector('#communityFlaggedCommentsModal').remove();
selectedCommunityId = mainId;
getAllCommunityPosts(mainId);
getAllCommunityEvents(mainId);
getAllCommunityPolls(mainId);

});

communityContainer.appendChild(newElement);
}

/**
 * Function to fetch all the community posts
 */
function getAllCommunityPosts(id) {
    $.ajax({
        url: `${communityURL}/getCommunityData/${id}/0`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    const fullData = data;
    data = data.filter(obj => obj.approved);
    if (data.length == 0) {
        makeEmptyPost(postsContainer, 'posts');
    }
    else {
        data.forEach(d => {
            populatePostsData(d, postsContainer);
    });
}
if (inchargeFor.includes(id)) {
    makeCheckPostsAndReportPosts(fullData);
}
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

/**
 * Function to make all the events visible 
 */
function getAllCommunityEvents(id) {
    $.ajax({
        url: `${communityURL}/getCommunityData/${id}/1`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    const fullData = data;
    data = data.filter(obj => obj.approved);
    if (data.length == 0) {
        //makeEmptyPost(postsContainer,'events');
    }
    else {
        data.forEach(d => {
            populatePostsData(d, postsContainer, 'event');
    });
}
if (inchargeFor.includes(id)) {
    makeCheckPostsAndReportPosts(fullData, 'event');
}
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

/**
 * Function to make all the events visible 
 */
function getAllCommunityPolls(id) {
    $.ajax({
        url: `${communityURL}/getCommunityData/${id}/2`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    const fullData = data;
    data = data.filter(obj => obj.approved);
    if (data.length == 0) {
        //makeEmptyPost(postsContainer,'polls');
    }
    else {
        data.forEach(d => {
            populatePollData(d, postsContainer, false);
    });
    data.filter(obj => obj.createdBy == userId).forEach(d => {
        communityMyPollsContainer.innerHTML = '';
    populatePollData(d, communityMyPollsContainer, true);
});
}
if (inchargeFor.includes(id)) {
    makeCheckPostsAndReportPosts(fullData, 'poll');
}
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

/**
 * Function to make and show a poll
 */
function populatePollData(data, container, modalFlag) {
    const newElement = document.createElement('div');
    newElement.classList.add('card', 'mb-2', 'post-body', 'community-poll-item');
    newElement.id = data._id;
    newElement.dataset.isApproved = data.approved;
    const member = allMembersOA.filter(obj => obj.userId == data.createdBy)[0];
    const pollOptions = populatePollOptions(data.pollOptions, modalFlag);
    newElement.innerHTML = `
    <div class="card-body">
        <!--  -->
        <div class="row">
        <!--  -->
        <div class="col-8">
            <div class="d-flex align-items-center justify-content-start">
            <img alt="image" class="rounded post-image" width="70" src="${member.memberImage}" defer>
            <svg src="" alt="" width="30px" height="30px" class="rounded post-image post-svg hide" data-jdenticon-value="${member.memberName}"></svg>
            <div class="ml-2">
                <div class=" fw-bold">${member.memberName}</div>
                <i class="bi bi-clock-history secondary-text"></i>
                <span class="secondary-text">${timeSince(data.creationDateAndTime)}</span>
            </div>
            </div>
        </div>
        <!--  -->
        <div class="col-4">
            <div class="text-right">
            <div class="post-options">              
            </div>            
            </div>                          
        </div>
        <!--  -->
        <class="col-12 my-2">
        <!--  -->
        <div class="col-12">
            <h6>${data.pollTitle}</h6>
        </div>
        <div class="col-12">
            ${pollOptions.list}
    </div>
    <!--  -->
</div>
    `;

    const flag = pollOptions.flag;

    const allRadioBtns = newElement.querySelectorAll('[name="poll-ratio-group"]')

    if (flag) {
        allRadioBtns.forEach(rb => rb.disabled = true);
    }
    else {
        const submitAnswer = newElement.querySelector('.submit-community-poll-btn');
        submitAnswer.addEventListener('click', () => {
            const checkedBtn = newElement.querySelector('input[name="poll-ratio-group"]:checked');
        if (!checkedBtn) {
            iziToast.error({ message: 'Please select an option!' });
            return;
        }
            const id = checkedBtn.id;
        $.ajax({
            url: `${communityURL}/addPostOptionChoice/${selectedCommunityId}/${data._id}/${id}/${userId}`,
    method: "PATCH",
    contentType: "application/json",
    success: function (data) {
        allRadioBtns.forEach(rb => rb.disabled = true);
        submitAnswer.parentElement.innerHTML = `<span class="badge badge-success"> Thank you for your answer!</span>`;
        iziToast.success({ message: 'Poll answer saved!' });
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
});
}
// <code> You have already answered this!</code>

    const image = newElement.querySelector('.post-image');
    const svg = newElement.querySelector('.post-svg');
image.onerror = () => {
    image.classList.add('hide');
svg.classList.remove('hide');
jdenticon();
}

container.prepend(newElement);
}

/**
 * Function to populate all the poll options
 */
function populatePollOptions(arr, stats = false) {
    let flag = false;
    const optionArr = ['<div>'];
    const allOptions = [];
    arr.forEach(option => option.answeredBy.forEach(val => allOptions.push(val)));
    arr.forEach(option => {
        option.answeredBy.includes(userId) ? flag = true : '';
        const optionPercent = Math.ceil(option.answeredBy.length / allOptions.length * 100);
    optionArr.push(`
    <div class="custom-control custom-radio d-flex justify-content-between align-items-center">
        <div class="">
            <input type="radio" id="${option._id}" name="poll-ratio-group" class="custom-control-input" ${option.answeredBy.includes(userId) ? 'checked' : ''} ${stats ? 'disabled' : ''}>
            <label class="custom-control-label font-14" for="${option._id}">${option.option}</label>
        </div>
    ${stats ? `<div class="d-flex justify-content-end align-items-center">
        <div class="progress" style="min-width:150px;box-shadow:none;height:10px;">
            <div class="progress-bar font-10 align-items-center ${optionPercent <= 33.33 ? 'bg-danger' : optionPercent > 33.33 && optionPercent <= 66.66 ? 'bg-warning' : 'bg-success'}" role="progressbar" aria-valuemin="0" aria-valuemax="100" style="width:${optionPercent}%;height:10px;line-height: 10px;"></div>                    
        </div>
        <span class="ms-1 font-12" style="line-height:10px; min-width:50px;">${option.answeredBy.length} (${optionPercent ? optionPercent : 0}%)</span>
    </div>`: ''}
</div>
        `);
});
if (flag) {
    optionArr.push(`<div class="text-center">
        <span class="badge badge-success"> You have already answered this!</span>
    </div>`);
}
else {
    optionArr.push(`<div class="text-center">
        <button type="button" class="btn btn-outline-primary btn-sm submit-community-poll-btn">Submit</button>
    </div>`);
}

optionArr.push('</div>');
return {
    list: optionArr.join(''),
    flag
    };

}

/**
 * Function to verify posts and flagged messages 
 */
function makeCheckPostsAndReportPosts(data, type = '') {
    const generalTab = document.querySelector('[data-community-general="true"]');
    //${generalTab.classList.contains('active') ? '' : 'hide'}  
    const newElement = document.createElement('div');
    newElement.className = 'card text-center mb-2 admin-options';
    newElement.innerHTML = `
        <div class="card-body">
            <button class="btn btn-outline-primary" type="button" data-toggle="modal" data-target="#communityUnverifiedPostsModal">
                <i class="bi bi-clipboard-check-fill"></i>
    Verify Posts
</button>                   
<button class="btn btn-outline-primary verify-community-members " type="button">
    <i class="bi bi-people-fill"></i>
    Verify Members
</button> 
<button class="btn btn-outline-danger reported-community-comments" type="button">
    <i class="bi bi-exclamation-triangle-fill"></i>
    Reported Comments
</button>      
<button class="btn btn-outline-info refresh-community-data" type="button">
    <i class="bi bi-arrow-clockwise"></i>
    Refresh Data
</button>           
</div>
        
    `;
    makeModalForUnverifiedPosts(data, type);

    const verifyMembersBtn = newElement.querySelector('.verify-community-members');
    verifyMembersBtn.addEventListener('click', () => {
        if (document.querySelector('#verifyAllMembers'))
            document.querySelector('#verifyAllMembers').remove();
    makeModalForVerifyingMembers(allMembersIC);
    $('#verifyAllMembers').modal('show');
});

    const reportedCommentsBtn = newElement.querySelector('.reported-community-comments');
    reportedCommentsBtn.addEventListener('click', checkCommentValidity);

    const refreshDataBtn = newElement.querySelector('.refresh-community-data');
    refreshDataBtn.addEventListener('click', () => {
        modalPostContainer.innerHTML = ``;
    modalEventContainer.innerHTML = ``;
    modalPollContainer.innerHTML = ``;
    postsContainer.innerHTML = ``;
    getAllCommunityPosts(selectedCommunityId);
    getAllCommunityEvents(selectedCommunityId);
    getAllCommunityPolls(selectedCommunityId);
});
if (!document.querySelector('.refresh-community-data'))
    document.querySelector('.community-admin-panel').prepend(newElement);
}

/**
 * Function to check the validity of the comments
 */
function checkCommentValidity() {
    $.ajax({
        url: `${communityURL}/getFlaggedComments/${selectedCommunityId}`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    if (document.querySelector('#evaluateReportedComments')) document.querySelector('#evaluateReportedComments').remove();
    makeModalForCheckingComments(data.comments);
    $('#evaluateReportedComments').modal('show');
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

/**
 * function to make the flags for all the 
 */
function makeModalForCheckingComments(data) {
    const newElement = document.createElement('div');
    newElement.className = 'modal fade';
    newElement.id = "evaluateReportedComments";
    newElement.innerHTML = `
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Check Comments</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
                <div class="row">
                    ${makeReportedCommentElement(data)}
    </div>
</div>            
</div>
</div>
    `;

    const allDeleteIcons = newElement.querySelectorAll('.delete-icon');
    allDeleteIcons.forEach((icon) => {
        tippy(icon, {
            arrow: true,
            theme: 'Logout',
        });
    icon.addEventListener('click', () => {
        const commentId = icon.dataset.commentVerifyId;
            const parentId = icon.dataset.communityCategoryId;
            const type = icon.dataset.communityCategoryType;
    Swal.fire({
        title: `Delete this comment?`,
        html: "<code> You won't be able to revert this!</code>",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, Delete!'
}).then((result) => {
    if (result.isConfirmed) {
        $.ajax({
    url: `${communityURL}/deleteComment/${selectedCommunityId}/${parentId}/${commentId}/${type}`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    icon.closest('.col-12').remove();
    Swal.fire(
        'Comment Deleted!',
        '',
        'success'
    )
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
});
});
});

    const allEditIcons = newElement.querySelectorAll('.edit-icon');
allEditIcons.forEach((icon) => {
    tippy(icon, {
        arrow: true,
        theme: 'otika',
    });
icon.addEventListener('click', () => {
    const commentId = icon.dataset.commentVerifyId;
            const parentId = icon.dataset.communityCategoryId;
            const type = icon.dataset.communityCategoryType;
Swal.fire({
    title: `Allow this comment?`,
    html: "<code>This will make this comment visible to all users!</code>",
icon: 'warning',
showCancelButton: true,
confirmButtonColor: '#3085d6',
cancelButtonColor: '#d33',
confirmButtonText: 'Yes, Approve!'
}).then((result) => {
    if (result.isConfirmed) {
        $.ajax({
    url: `${communityURL}/approveComment/${selectedCommunityId}/${parentId}/${commentId}/${type}`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    icon.closest('.col-12').remove();
    Swal.fire(
        'Comment Approved!',
        '',
        'success'
    )
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
});
});
});

    const allTextToTruncate = newElement.querySelectorAll('.truncate-this');
allTextToTruncate.forEach(text => {
    toggleText(text, 130);
})

    const allImages = newElement.querySelectorAll('.memberCommentImage');
    const allSVGs = newElement.querySelectorAll('.memberCommentSvg');
allImages.forEach((image, i) => {
    image.onerror = () => {
        image.classList.add('hide');
allSVGs[i].classList.remove('hide');
jdenticon();
}
})

document.body.appendChild(newElement);
}

/**
 * FUnction to make the reported comments
 */
function makeReportedCommentElement(arr) {
    const allComments = [];
    if (arr.length == 0) {
        return `<code class="col-12 text-center font-14">No Reported Comments Yet!</code>`;
    }
    arr.forEach(comment => {
        const createdByUser = allMembersOA.filter(obj => obj.userId == comment[1].createdBy)[0];
        const flagRaisedByUser = allMembersOA.filter(obj => obj.userId == comment[1].flagRaisedBy)[0];
        const commentBody = `
            <div class="col-lg-12 col-md-6 col-12 mb-2" data-comment-verify-id="${comment._id}">
                <div class="post-type text-start">
                    <div class="row">
                        <div class="col-11 truncate-this">
                            <b><span class="text">${comment[1].commentDesc}</span></b>
                        </div>                         
                        <div class="col-1 ps-0">
                            <i class="bi bi-check-square font-16 option-icon edit-icon tippy" data-tippy-content="Allow Comment" data-community-category-id="${comment[0]}" data-comment-verify-id="${comment[1]._id}" data-community-category-type="${comment[2]}"></i>
                            <i class="bi bi-trash font-16 ml-2 option-icon delete-icon tippy" data-tippy-content="Delete Comment" data-community-category-id="${comment[0]}" data-comment-verify-id="${comment[1]._id}" data-community-category-type="${comment[2]}"></i>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <div class="col-12 font-11">Posted By:</div>
                            <img alt="image" class="msr-2 rounded memberCommentImage" width="40px" height="40px" src="${createdByUser.memberImage}" defer>
                            <svg src="" alt="" width="30px" height="30px" class="msr-2 rounded hide memberCommentSvg bg-white" data-jdenticon-value="${createdByUser.memberName}"></svg>
                            <b>${createdByUser.memberName}</b>
                        </div>
                        <div class="col-6">
                            <div class="col-12 font-11">Flag Raised By:</div>
                            <img alt="image" class="msr-2 rounded memberCommentImage" width="40px" height="40px" src="${flagRaisedByUser.memberImage}" defer>
                            <svg src="" alt="" width="30px" height="30px" class="msr-2 rounded hide memberCommentSvg bg-white" data-jdenticon-value="${flagRaisedByUser.memberName}"></svg>
                            <b>${flagRaisedByUser.memberName}</b>
                        </div>
                    </div>
                </div>
            </div>
            
        `;
    allComments.push(commentBody);
});

return allComments.join('');
}

/**
 * function to make the modal for use verification
 */
function makeModalForVerifyingMembers(allMembersIC) {
    const newElement = document.createElement('div');
    newElement.id = "verifyAllMembers";
    newElement.className = 'modal fade';
    const verifiedMembers = allMembersIC.filter(obj => obj.isVerified);
    const unverifiedMembers = allMembersIC.filter(obj => !obj.isVerified);
    newElement.innerHTML = `
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">All Members</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <!-- -->
                    <div class="col-lg-6 col-md-6 col-12 verify-left-container">
                        <h6>Verified</h6>
                        <class="my-2">
                        ${makeMembersInACommunity(verifiedMembers)}
    </div>
    <!-- -->
    <div class="col-lg-6 col-md-6 col-12 verify-right-container">
        <h6>Unverified</h6>
        <class="my-2">
        ${makeMembersInACommunity(unverifiedMembers)}
    </div>
    <!-- -->
</div>
</div>  
                
</div>
    `;
    const leftSideContainer = newElement.querySelector('.verify-left-container');

    const rightSideContainer = newElement.querySelector('.verify-right-container');

    const allImages = newElement.querySelectorAll('.memberVerifyImage');
    const allSVG = newElement.querySelectorAll('.memberVerifySvg');
    allImages.forEach((image, i) => {
        image.onerror = () => {
            image.classList.add('hide');
    allSVG[i].classList.remove('hide');
    jdenticon();
}
});

    const allApprovalBtns = newElement.querySelectorAll('.approveCommunityMember');
    const allRefuteBtns = newElement.querySelectorAll('.removeCommunityMember');

allApprovalBtns.forEach((btn, i) => {
    btn.addEventListener('click', () => {
        const id = btn.dataset.communityMemberId;
let member = [{
    memberName: btn.dataset.commMemberName,
    memberImage: btn.dataset.commMemberImage,
    _id: btn.dataset.memberServerId
}];
$.ajax({
    url: `${communityURL}/switchMemberStatus/${selectedCommunityId}/${id}/1`,
    method: "PATCH",
contentType: "application/json",
success: function (data) {
    btn.classList.add('hide');
    allRefuteBtns[i].classList.remove('hide');
    $(btn.closest('.post-type')).appendTo(leftSideContainer);
    makeMembersInACommunity(member, communityMemberContainer);
    document.querySelector('#community-member-count span').textContent = Number(document.querySelector('#community-member-count span').textContent) + 1;
    iziToast.success({ message: 'Member Approved!' });
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
});
});

allRefuteBtns.forEach((btn, i) => {
    btn.addEventListener('click', () => {
        const id = btn.dataset.communityMemberId;
            const listId = btn.dataset.memberServerId;
$.ajax({
    url: `${communityURL}/switchMemberStatus/${selectedCommunityId}/${id}/0`,
    method: "PATCH",
contentType: "application/json",
success: function (data) {
    communityMemberContainer.querySelector(`li[data-community-member-display-id="${listId}"]`).remove();
    btn.classList.add('hide');
    allApprovalBtns[i].classList.remove('hide');
    $(btn.closest('.post-type')).appendTo(rightSideContainer);
    document.querySelector('#community-member-count span').textContent = Number(document.querySelector('#community-member-count span').textContent) - 1;
    iziToast.warning({ message: 'Member Removed!' });
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
});
});

document.body.appendChild(newElement);
}

/**
 * Function  to make all the unverified members
 */
function makeMembersInACommunity(dataArr, container = '') {
    const allMembers = [];
    dataArr.forEach(member => {
        if (container != '') {
            const newElement = document.createElement('li');
    newElement.className = 'items-list-compo';
    newElement.dataset.communityMemberDisplayId = member._id;
    newElement.innerHTML = `
        <img alt="image" class="msr-2 rounded memberImage" width="40px" height="40px" src="${member.memberImage}" defer>
        <svg src="" alt="" width="30px" height="30px" class="msr-2 rounded post-image post-svg hide memberSvg" data-jdenticon-value="${member.memberName}"></svg>  
        <div class="media-body">
            <div class="mt-0 fw-bold text-capitalize">
                ${member.memberName}
    </div>
    <div class="text-success text-small font-600-bold">
        <!-- <i class="fas fa-circle"></i> Online -->
    </div>
</div>
            `;
            const image = newElement.querySelector('.memberImage');
            const svg = newElement.querySelector('.memberSvg');
    image.onerror = () => {
        image.classList.add('hide');
    svg.classList.remove('hide');
    jdenticon();
}
    container.appendChild(newElement);
}
else if (allMembersOA.filter(obj => obj.userId == member.memberId)[0]) {
    const memberDetails = allMembersOA.filter(obj => obj.userId == member.memberId)[0];
    allMembers.push(`
        <div class="post-type py-2 d-flex justify-content-between align-items-center mb-2">
            <div class="d-flex g-1 align-items-center ">
                <img alt="image" class="msr-2 rounded memberVerifyImage" width="40px" height="40px" src="${memberDetails.memberImage}" defer>
                <svg src="" alt="" width="30px" height="30px" class="msr-2 rounded post-image post-svg hide memberVerifySvg bg-white" data-jdenticon-value="${memberDetails.memberName}"></svg>  
                <div class="community-name font-16"> ${memberDetails.memberName}</div>
            </div>
            <div class="">
                <button type="button" class="btn btn-primary btn-sm approveCommunityMember ${!member.isVerified ? '' : 'hide'}" data-comm-member-image="${memberDetails.memberImage}" data-comm-member-name="${memberDetails.memberName}" data-community-member-id="${member._id}" data-member-server-id="${memberDetails._id}">+ Approve</button> 
                <button type="button" class="btn btn-outline-danger btn-sm removeCommunityMember ${!member.isVerified ? 'hide' : ''}" data-comm-member-image="${memberDetails.memberImage}" data-comm-member-name="${memberDetails.memberName}" data-community-member-id="${member._id}" data-member-server-id="${memberDetails._id}">- Remove</button>
            </div>
        </div>
    `);
}
});

if (container == '') {
    return allMembers.join('')
}
}



/**
 * Make the modal for unverified posts
 */
function makeModalForUnverifiedPosts(data, type = '') {
    if (!document.querySelector('#communityUnverifiedPostsModal')) {
        const newElement = document.createElement('div');
        newElement.className = 'modal fade';
        newElement.id = 'communityUnverifiedPostsModal';
        newElement.innerHTML = `
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Unverified Posts</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
            <div class="modal-body">
                <ul class="nav nav-tabs" id="myTab" role="tablist">
                    <li class="nav-item">
                    <a class="nav-link active" id="community-posts-tab" data-toggle="tab" href="#community-posts" role="tab" aria-controls="community-posts" aria-selected="true">Posts</a>
                    </li>
                    <li class="nav-item">
                    <a class="nav-link" id="community-events-tab" data-toggle="tab" href="#community-events" role="tab" aria-controls="Events" aria-selected="false">Events</a>
                    </li>
                    <li class="nav-item">
                    <a class="nav-link" id="community-polls-tab" data-toggle="tab" href="#community-polls" role="tab" aria-controls="Polls" aria-selected="false">Polls</a>
                    </li>
                </ul>
                <div class="tab-content mt-3" id="myTabContent">
                    <div class="tab-pane fade show active" id="community-posts" role="tabpanel" aria-labelledby="community-posts-tab">
                    
                    </div>
                    <div class="tab-pane fade" id="community-events" role="tabpanel" aria-labelledby="community-events-tab">
                    
                    </div>
                    <div class="tab-pane fade" id="community-polls" role="tabpanel" aria-labelledby="community-polls-tab">
                    
                    </div>
                </div>
            </div>
        </div>
        `;
        modalPostContainer = newElement.querySelector('#community-posts');
        modalPostContainer.innerHTML = ``;

        modalEventContainer = newElement.querySelector('#community-events');
        modalEventContainer.innerHTML = ``;

        modalPollContainer = newElement.querySelector('#community-polls');
        modalPollContainer.innerHTML = ``;

        document.body.appendChild(newElement);
    }

    if (data.length == 0) {
        if (type == 'event') {
            makeEmptyPost(modalEventContainer, 'events');
        }
        else if (type == 'poll') {
            makeEmptyPost(modalPollContainer, 'polls');
        }
        else {
            makeEmptyPost(modalPostContainer, 'posts');
        }
    }
    else {
        if (type == 'event') {
            data.filter(obj => !obj.approved).forEach(d => {
                populatePostsData(d, modalEventContainer, 'event');
        });
        makeAllPostsVerifiable(modalEventContainer, 'event');
    }
else if (type == 'poll') {
    data.filter(obj => !obj.approved).forEach(d => {
        populatePollData(d, modalPollContainer, false);
});
    makeAllPostsVerifiable(modalPollContainer, 'poll');
}
else {
            data.filter(obj => !obj.approved).forEach(d => {
                populatePostsData(d, modalPostContainer);
});
makeAllPostsVerifiable(modalPostContainer);
}
}
}

/**
 * Function to make all posts verifiable
 */
function makeAllPostsVerifiable(container, type = '') {
    let allPosts;
    if (type == 'event') {
        allPosts = document.querySelectorAll('.community-event-item');
    }
    else if (type == 'poll') {
        allPosts = document.querySelectorAll('.community-poll-item');
    }
    else {
        allPosts = document.querySelectorAll('.community-post-item');
    }
    allPosts.forEach(post => {
        const postOptions = post.querySelector('.post-options');
        const isApproved = post.dataset.isApproved;
    postOptions.innerHTML = `
        <i class="bi bi-check-square verify-post option-icon edit-icon font-20 mr-2 tippy ${isApproved == "false" ? '' : 'hide'}" data-tippy-content="Verify Post"></i>
        <i class="bi bi-x-square refute-post option-icon warning-icon font-20 mr-2 tippy ${isApproved == "false" ? 'hide' : ''}" data-tippy-content="Refute Post"></i>
        <i class="bi bi-trash delete-post option-icon delete-icon font-20 tippy" data-tippy-content="Delete Post"></i>
        `;
        const verifyPostBtn = postOptions.querySelector('.verify-post');
        const refutePostBtn = postOptions.querySelector('.refute-post');
        const deletePostBtn = postOptions.querySelector('.delete-post');
        const id = post.id;

    tippy(verifyPostBtn, {
        arrow: true,
        theme: 'otika',
    });
    tippy(deletePostBtn, {
        arrow: true,
        theme: 'Logout',
    });
    tippy(refutePostBtn, {
        arrow: true,
        theme: 'Help',
    });

    verifyPostBtn.addEventListener('click', (e) => {
        e.stopPropagation();
    if (type == 'event') {
        $.ajax({
            url: `${communityURL}/approveEvent/${selectedCommunityId}/${id}/1`,
    method: "PATCH",
    contentType: "application/json",
    success: function (data) {
        verifyPostBtn.classList.add("hide");
        refutePostBtn.classList.remove("hide");
        post.dataset.isApproved = true;
        if (postsContainer.querySelector('.empty-post')) {
            postsContainer.querySelector('.empty-post').remove();
        }
        $(post).appendTo($(postsContainer));
        if (container.querySelectorAll('.card:not(.empty-post)').length == 0) {
            makeEmptyPost(container, 'events');
        }
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.error('error', XMLHttpRequest)
    }
});
}
else if (type == 'poll') {
    $.ajax({
        url: `${communityURL}/approvePoll/${selectedCommunityId}/${id}/1`,
method: "PATCH",
contentType: "application/json",
success: function (data) {
    verifyPostBtn.classList.add("hide");
    refutePostBtn.classList.remove("hide");
    post.dataset.isApproved = true;
    if (postsContainer.querySelector('.empty-post')) {
        postsContainer.querySelector('.empty-post').remove();
    }
    $(post).appendTo($(postsContainer));
    if (container.querySelectorAll('.card:not(.empty-post)').length == 0) {
        makeEmptyPost(container, 'polls');
    }
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
else {
    $.ajax({
        url: `${communityURL}/approvePost/${selectedCommunityId}/${id}/1`,
method: "PATCH",
contentType: "application/json",
success: function (data) {
    verifyPostBtn.classList.add("hide");
    refutePostBtn.classList.remove("hide");
    post.dataset.isApproved = true;
    if (postsContainer.querySelector('.empty-post')) {
        postsContainer.querySelector('.empty-post').remove();
    }
    $(post).appendTo($(postsContainer));
    if (container.querySelectorAll('.card:not(.empty-post)').length == 0) {
        makeEmptyPost(container, 'posts');
    }
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
});

refutePostBtn.addEventListener('click', (e) => {
    e.stopPropagation();
if (type == 'event') {
    $.ajax({
        url: `${communityURL}/approveEvent/${selectedCommunityId}/${id}/0`,
method: "PATCH",
contentType: "application/json",
success: function (data) {
    verifyPostBtn.classList.remove("hide");
    refutePostBtn.classList.add("hide");
    post.dataset.isApproved = false;
    if (container.querySelector('.empty-post')) {
        container.querySelector('.empty-post').remove();
    }
    $(post).prependTo($(container));
    if (postsContainer.querySelectorAll('.card:not(.empty-post):not(.admin-options)').length == 0) {
        makeEmptyPost(postsContainer, 'events');
    }
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
else if (type == 'poll') {
    $.ajax({
        url: `${communityURL}/approvePoll/${selectedCommunityId}/${id}/0`,
method: "PATCH",
contentType: "application/json",
success: function (data) {
    verifyPostBtn.classList.remove("hide");
    refutePostBtn.classList.add("hide");
    post.dataset.isApproved = false;
    if (container.querySelector('.empty-post')) {
        container.querySelector('.empty-post').remove();
    }
    $(post).prependTo($(container));
    if (postsContainer.querySelectorAll('.card:not(.empty-post):not(.admin-options)').length == 0) {
        makeEmptyPost(postsContainer, 'polls');
    }
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
else {
    $.ajax({
        url: `${communityURL}/approvePost/${selectedCommunityId}/${id}/0`,
method: "PATCH",
contentType: "application/json",
success: function (data) {
    verifyPostBtn.classList.remove("hide");
    refutePostBtn.classList.add("hide");
    post.dataset.isApproved = false;
    if (container.querySelector('.empty-post')) {
        container.querySelector('.empty-post').remove();
    }
    $(post).prependTo($(container));
    if (postsContainer.querySelectorAll('.card:not(.empty-post):not(.admin-options)').length == 0) {
        makeEmptyPost(postsContainer, 'posts');
    }
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
});

deletePostBtn.addEventListener('click', (e) => {
    e.stopPropagation();
Swal.fire({
    title: `Delete this post?`,
    html: "<code> You won't be able to revert this!</code>",
icon: 'warning',
showCancelButton: true,
confirmButtonColor: '#3085d6',
cancelButtonColor: '#d33',
confirmButtonText: 'Yes, Delete!'
}).then((result) => {
    if (result.isConfirmed) {
        if (type == 'event') {
            $.ajax({
                url: `${communityURL}/deleteEvent/${selectedCommunityId}/${id}`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    post.remove();
    Swal.fire(
        'Event Deleted!',
        '',
        'success'
    )
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
else if (type == 'poll') {
    $.ajax({
        url: `${communityURL}/deletePoll/${selectedCommunityId}/${id}`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    post.remove();
    Swal.fire(
        'Poll Deleted!',
        '',
        'success'
    )
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
else {
    $.ajax({
        url: `${communityURL}/deletePost/${selectedCommunityId}/${id}`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    post.remove();
    Swal.fire(
        'Post Deleted!',
        '',
        'success'
    )
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

}
});

});

});
}

/**
 * Function to make an empty post
 */
function makeEmptyPost(container, name) {
    const newElement = document.createElement('div');
    newElement.classList.add('card', 'mb-2', 'empty-post');
    newElement.innerHTML = `
    <div class="card-body ">
        <img src="../Images/Community/no_${name}_yet.jpg" class="img-fluid d-block m-auto"  alt="No ${name} available" defer></img>
    </div>
    `;
    container.appendChild(newElement);
}

/**
 * Function to make a single image
 */
function makeASingleCommunityImage(image, className = "", element = '', style = '') {
    return `
    <a href="${communityBlobContainer}/${orgId}/${collegeId}/${image}" class="community-fit-image ${className}" data-sub-html="${image.split('_')[3]}" style="${style}">
        ${element}
    <img class="img-fluid thumbnail community-post-image" src="${communityBlobContainer}/${orgId}/${collegeId}/${image}" alt="${image}" defer>
</a>
    `;
}

    /**
     * Function to make a post images
     */
    function makeCommunityImageElement(imageArr) {
        if (imageArr.length == 0) return '';
        const images = [];
        if (imageArr.length == 1) {
            imageArr.forEach(image => {
                images.push(makeASingleCommunityImage(image, '', '', 'width:100%;max-height:225px;'));
        });
    }
else if (imageArr.length == 2 || imageArr.length == 4) {
    images.push('<div class="preview-2-images">');
    imageArr.forEach(image => {
        images.push(makeASingleCommunityImage(image));
});
    images.push('</div>');
}
else if (imageArr.length == 3) {
    imageArr.forEach((image, i) => {
        if (i == 0) {
            images.push(makeASingleCommunityImage(image, 'w-100 d-flex align-items-center justify-content-center'));
}
else if (i == 1) {
    images.push('<div class="preview-2-images">');
    images.push(makeASingleCommunityImage(image));
}
else {
                images.push(makeASingleCommunityImage(image));
images.push('</div>');
}
});
images.push('</div>');
}
else {
    images.push('<div class="preview-2-images">');
    imageArr.forEach((image, i) => {
        if (i < 3)
            images.push(makeASingleCommunityImage(image));
    if (i == 3) {
        images.push(makeASingleCommunityImage(image, '', `<div class="community-image-overlay">+${imageArr.length - 4}</div>`));
}
if (i > 3) {
    images.push(makeASingleCommunityImage(image, 'd-none'));
}
});
images.push('</div>');
}
return images.join('');
}

/**
 * Function to make a post video
 */
function makeCommunityVideoElement(imageArr) {
    if (imageArr.length == 0) return '';
    const videos = [];
    imageArr.forEach(image => {
        videos.push(`
            <video class="w-100" 
                    src="${communityBlobContainer}/${orgId}/${collegeId}/${image}"
                    controls 
                    onloadedmetadata="this.muted = true"
                    id="community-event-video-preview"
                    onmouseenter="play()"                 
                    style="grid-column:span 5;max-height:325px;"
            >
                Your browser does not support the video tag.
            </video>
        `);
});
return videos.join('');
}


/**
 * FUnction to populate the posts data
 */
function populatePostsData(data, container, type = '') {
    console.log(data)
    const filteredComments = data.comments.filter(comment => comment.raiseFlag == false);
    const newElement = document.createElement('div');
    newElement.classList.add('card', 'mb-2', 'post-body');
    type == 'event' ? newElement.classList.add('community-event-item') : newElement.classList.add('community-post-item');
    newElement.id = data._id;
    newElement.dataset.isApproved = data.approved;
    const member = allMembersOA.filter(obj => obj.userId == data.createdBy)[0];
    newElement.innerHTML = `
    <div class="card-body">
      <!--  -->
      <div class="row">
        <!--  -->
        <div class="col-8">
          <div class="d-flex align-items-center justify-content-start">
            <img alt="image" class="rounded post-image" width="70" src="${member.memberImage}" defer>
            <svg src="" alt="" width="30px" height="30px" class="rounded post-image post-svg hide" data-jdenticon-value="${member.memberName}"></svg>
            <div class="ml-2">
              <div class=" fw-bold">${member.memberName}</div>
              <i class="bi bi-clock-history secondary-text"></i>
              <span class="secondary-text">${timeSince(data.creationDateAndTime)}</span>
            </div>
          </div>
        </div>
        <!--  -->
        <div class="col-4">
          <div class="text-right">
            <div class="post-options">              
            </div>            
          </div>                          
        </div>
        <!--  -->
        <div class="col-12 my-2">
        <!--  -->
        ${type == 'event' ? `
            <div class="col-12 ">
                <h6><span>${data.eventTitle}</span>
                </h6>
                <div class="d-flex justify-content-between align-items-center mb-2">
                    ${data.eventDateStart && data.eventDateEnd  ? `<span><i class="bi bi-clock font-16 me-1"></i> <b>${moment(data.eventDateStart).format('lll')} - ${moment(data.eventDateEnd).format('lll')}</b></span>` : '' }
                    <div>
                        <b class="badge badge-success font-12">${data.eventType == 1 ? 'Online' : 'Offline'}</b>
                        <b class="badge badge-primary font-12">${data.price == 0 ? 'FREE' : `Rs. ${data.price}`}</b>
                    </div>
                </div>
            ${data.venue ? `<div class="col-12 mb-2"> <i class="bi bi-geo-alt font-16"></i> <b>${data.venue}</b></div>` : ''}
                ${data.url ? `<a href="${data.url}" class="d-block mb-2" target="_blank">${data.url}</a>` : ''} 
            </div>`
                    : ''}
        
                <div class="col-12 truncate-this">
                  <span class='text'>${type == 'event' ? data.eventDesc : data.postDescription}</span>
                </div>
                <div class="col-12">
                  <div class="text-center mt-2 bg-black posts-image-container">
                    ${data.isVideo ? makeCommunityVideoElement(data.filePath) : makeCommunityImageElement(data.filePath)}
                </div>                          
              </div>
              <!--  -->
              
              <!--  -->
              <div class="col-12 border-top">
                <div class="d-flex align-items-center justify-content-between">
                  <div class="left-side-post font-14 addIcon thumbs-up-btn" data-no-of-likes-id="${data._id}">
                    <i class="bi ${data.noOfLikes.includes(userId) ? 'blue-icon bi-hand-thumbs-up-fill' : 'bi-hand-thumbs-up'} thumbs-up-community" ></i>
                    <span class="thumbs-up-count-community">${data.noOfLikes.length}</span>
                  </div>      
                  <div class="middle-post font-14 addIcon">
                    <i class="bi bi-reply font-18 reply-community"></i>
                  </div>           
                  <div class="right-side-post addIcon message-community">
                    <i class="bi bi-chat-right-text font-14"></i>
                    <span class="message-count-community">${filteredComments.length}</span>
                  </div>
                </div>
              </div>
                   
              <div class="col-12 mt-2 hide post-others-messages">

              </div>                        
              <!--  -->
              <div class="col-12 mt-2 post-something hide">
                <div class="reply d-flex">
                  <input type="text" class="form-control post-a-reply-input" placeholder="Post a reply?">
                  <button type="button" class="btn btn-outline-primary post-a-reply-btn" data-post-message-in-post-id="${data._id}">
                    <i class="bi bi-reply font-18"></i>
                  </button>
                </div>
              </div>
              <!--  -->
            </div>
            <!--  -->
          </div>
    
`;


const image = newElement.querySelector('.post-image');
const svg = newElement.querySelector('.post-svg');
                image.onerror = () => {
                    image.classList.add('hide');
                svg.classList.remove('hide');
                jdenticon();
            }

    const allCommunityImages = newElement.querySelectorAll('.community-fit-image');
            allCommunityImages.forEach(image => image.addEventListener('click', () => { $(communityUnverifiedPostsModal).modal('hide') }));

    const thumbsUpBtn = newElement.querySelector('.thumbs-up-btn');
            thumbsUpBtn.addEventListener('click', () => {
                const icon = thumbsUpBtn.querySelector('i.bi');
        const parent = icon.parentElement;
        const count = parent.querySelector('span');
        const postId = thumbsUpBtn.dataset.noOfLikesId;
            if (icon.classList.contains('bi-hand-thumbs-up')) {
                if (type == 'event') {
                    $.ajax({
                        url: `${communityURL}/toggleEventThumbsUp/${selectedCommunityId}/${postId}/${userId}/1`,
                method: "PATCH",
                contentType: "application/json",
                success: function (data) {
                    count.textContent = Number(count.textContent) + 1;
                    icon.classList.add('blue-icon', 'bi-hand-thumbs-up-fill');
                    icon.classList.remove('bi-hand-thumbs-up');
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.error('error', XMLHttpRequest)
                }
            });
        }
else {
                $.ajax({
                    url: `${communityURL}/togglePostThumbsUp/${selectedCommunityId}/${postId}/${userId}/1`,
    method: "PATCH",
    contentType: "application/json",
    success: function (data) {
        count.textContent = Number(count.textContent) + 1;
        icon.classList.add('blue-icon', 'bi-hand-thumbs-up-fill');
        icon.classList.remove('bi-hand-thumbs-up');
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.error('error', XMLHttpRequest)
    }
});
}
}
else {
    if (type == 'event') {
        $.ajax({
            url: `${communityURL}/toggleEventThumbsUp/${selectedCommunityId}/${postId}/${userId}/0`,
    method: "PATCH",
    contentType: "application/json",
    success: function (data) {
        count.textContent = Number(count.textContent) - 1;
        icon.classList.remove('blue-icon', 'bi-hand-thumbs-up-fill');
        icon.classList.add('bi-hand-thumbs-up');
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.error('error', XMLHttpRequest)
    }
});
}
else {
    $.ajax({
        url: `${communityURL}/togglePostThumbsUp/${selectedCommunityId}/${postId}/${userId}/0`,
method: "PATCH",
    contentType: "application/json",
success: function (data) {
    count.textContent = Number(count.textContent) - 1;
    icon.classList.remove('blue-icon', 'bi-hand-thumbs-up-fill');
    icon.classList.add('bi-hand-thumbs-up');
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
}
});

    const toggleComments = newElement.querySelector('.message-community');
toggleComments.addEventListener('click', () => {
    toggleCommunityMessage(toggleComments);
});

    const otherPeopleMessagesContainer = newElement.querySelector('.post-others-messages');
filteredComments.forEach(comment => populateComments(comment, otherPeopleMessagesContainer, type));

    const toggleReplyBtn = newElement.querySelector('.reply-community');
toggleReplyBtn.addEventListener('click', () => {
    const replyContainer = toggleReplyBtn.closest('.card').querySelector('.post-something');
if (replyContainer.classList.contains('hide')) {
    replyContainer.classList.remove('hide');
}
else {
    replyContainer.classList.add('hide');
}
});

    const commentInput = newElement.querySelector('.post-a-reply-input');
    const commentBtn = newElement.querySelector('.post-a-reply-btn');

    const messageCount = newElement.querySelector('.message-count-community');

commentBtn.addEventListener('click', () => {
    if (!commentInput.value) {
        iziToast.error({ message: 'Comment cannot be empty!' });
return;
}
postAComment(data._id, commentInput.value, otherPeopleMessagesContainer, type);
toggleCommunityMessage(toggleComments);
messageCount.textContent = Number(messageCount.textContent) + 1;
});

toggleText(newElement.querySelector('.truncate-this'), 130);

container.prepend(newElement);
if (!data.isVideo) {
    $('.posts-image-container').lightGallery({
        thumbnail: true,
        selector: 'a'
    });
}

}


/**
 * Function to post a comment on the page
 */
function postAComment(id, post, container, type = '') {
    const packet = {
        commentDesc: post,
        creationDateAndTime: toIsoString(new Date()),
        createdBy: userId
    }
    if (type == 'event') {
        $.ajax({
            url: `${communityURL}/addAEventComment/${selectedCommunityId}/${id}`,
    method: "PATCH",
    data: JSON.stringify(packet),
    async: false,
    contentType: "application/json",
    success: function (data) {
        packet._id = data.id;
        populateComments(packet, container, 'event');
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.error('error', XMLHttpRequest)
    }
});
}
else {
        $.ajax({
            url: `${communityURL}/addAPostComment/${selectedCommunityId}/${id}`,
    method: "PATCH",
data: JSON.stringify(packet),
async: false,
contentType: "application/json",
success: function (data) {
    packet._id = data.id;
    populateComments(packet, container);
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}

}


/**
 * Function to check comments
 */
function populateComments(comment, container, type = '') {
    const newElement = document.createElement("div");
    newElement.className = "row align-items-center toggle-flag-hover mb-1";
    /**
     * ! add the url for the blob storage
     */
    const member = allMembersOA.filter(obj => obj.userId == comment.createdBy)[0];
    newElement.innerHTML = `
    <div class="col-2 text-center pe-0">
        <img alt="image" class="rounded post-image post-image-sm comment-image"  src="${member.memberImage}" defer>
        <svg src="" alt="" width="30px" height="30px" class="rounded post-image post-image-sm comment-svg hide" data-jdenticon-value="${member.memberName}"></svg>
    </div>
    <div class="col-9 ps-0">
        <div class="bg-secondary rounded-2 px-3 py-1 text-primary-black">
            <div class="fw-bold d-flex align-items-center justify-content-between"><span>${member.memberName}</span> <span class="secondary-text">${timeSince(comment.creationDateAndTime)}</span></div>
            <div class="font-12 truncate-this">
                <span class="text">${comment.commentDesc}</span>
            </div>                
        </div>
    </div>
    <div class="col-1">
        <i class="bi bi-flag-fill blue-icon raiseFlag tippy" data-tippy-content="Raise Issue" data-flag-id="${comment._id}"></i>
    </div>
    `;

    toggleText(newElement.querySelector('.truncate-this'), 130);

    const flag = newElement.querySelector('.raiseFlag');

    tippy(flag, {
        arrow: true,
        theme: 'otika',
    });

    flag.addEventListener('click', () => {
        const commentId = flag.dataset.flagId;
        const parentId = flag.closest('.card').id;
    toggleFlagOnOff(flag, parentId, commentId, type);
});

    const image = newElement.querySelector('.comment-image');
    const svg = newElement.querySelector('.comment-svg');
    image.onerror = () => {
        image.classList.add('hide');
    svg.classList.remove('hide');
    jdenticon();
}

container.prepend(newElement);

}


/**
 * Function to toggle flag
 */
function toggleFlagOnOff(flag, parentId, commentId, typeFlag = '') {
    const packet = {
        userId
        }
    const messageCount = flag.closest('.card').querySelector('.message-count-community');
    Swal.fire({
        title: `Raise a flag for this comment?`,
        html: "<div>This will remove the post from the community and will notify the administrator about this post.</div> <code> You won't be able to revert this!</code>",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, Raise a Flag!'
}).then((result) => {
    if (result.isConfirmed) {
        toggleCommunityMessage(flag);
    if (typeFlag == 'event') {
        $.ajax({
            url: `${communityURL}/toggleFlagForEventComment/${selectedCommunityId}/${parentId}/${commentId}`,
    method: "PATCH",
    data: JSON.stringify(packet),
    contentType: "application/json",
    success: function (data) {
        flag.closest('.row.toggle-flag-hover').remove();
        messageCount.textContent = Number(messageCount.textContent) - 1;
        Swal.fire(
            'Flag Raised!',
            'Your community administrators have been notified.',
            'success'
        )
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.error('error', XMLHttpRequest)
    }
});
}
else {
    $.ajax({
        url: `${communityURL}/toggleFlagForPostComment/${selectedCommunityId}/${parentId}/${commentId}`,
method: "PATCH",
    data: JSON.stringify(packet),
contentType: "application/json",
success: function (data) {
    flag.closest('.row.toggle-flag-hover').remove();
    messageCount.textContent = Number(messageCount.textContent) - 1;
    Swal.fire(
        'Flag Raised!',
        'Your community administrators have been notified.',
        'success'
    )
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    console.error('error', XMLHttpRequest)
}
});
}
}
});
}

/**
 * function to print all icons in the dom
 */
function populateAllCommunityIcons() {
    allCommunityIconList.forEach(icon => {
        const newElement = document.createElement('label');
    newElement.classList.add('selectgroup-item');
    newElement.innerHTML = `
        <input type="radio" name="community-icon-group" value="${icon}" class="selectgroup-input-radio">
        <span class="selectgroup-button d-flex align-items-center justify-content-center"><i class="bi ${icon}"></i></span>
    `;
    communityIconGroup.appendChild(newElement);
});
}



/**
 * Function to change the colors of the community list
 */
function toggleCommunityIconColors() {
    newCommunityIcon.classList.contains('dot-red') ? newCommunityIcon.classList.remove('dot-red') : '';
    newCommunityIcon.style.backgroundColor = this.value;
    newCommunityIcon.style.color = 'white';
    newCommunityIcon.dataset.iconColor = this.value;
}


/**
 * Function to change the icon of the Community list
 */
function toggleCommunityListIconContent() {
    newCommunityIcon.classList = `bi ${this.value} ${newCommunityIcon.classList.contains('dot-red') ? 'dot-red' : ''}`;
    newCommunityIcon.dataset.iconType = this.value;
}


/**
 * Function to reset the Community
 */
function makeNewCommunityModal() {
    editCommunityProperties.dataset.editCommunityId = '';
    communityName.value = '';
    document.querySelector('[name="community-color-group"][value="#F3565D"]').checked = true;
    document.querySelector('[name="community-icon-group"][value="bi-list-task"]').checked = true;
    newCommunityIcon.dataset.iconType = "bi-list-task";
    newCommunityIcon.classList = 'bi bi-list-task dot-red';
    newCommunityIcon.dataset.iconColor = "#F3565D";
    newCommunityIcon.style.backgroundColor = "#F3565D";
    newCommunityIcon.style.color = 'white';
    removeEditableCommunity();
}

/**
 * Function to remove lists being editable
 */
function removeEditableCommunity() {
    removeCommunityEditDetailsBtn.classList.add('hide');
    editAllCommunityBtn.classList.remove('hide');
    const allCommunities = communityContainer.querySelectorAll('.community-element:not(.general-community)');
    allCommunities.forEach(community => {
        community.querySelector('.bi-pencil-square').classList.add('hide');
    community.querySelector('.bi-trash').classList.add('hide');
});
}


/**
 * Function to make all lists editable
 */
function makeAllCommunityEditable() {
    removeCommunityEditDetailsBtn.classList.remove('hide');
    editAllCommunityBtn.classList.add('hide');
    const allCommunities = communityContainer.querySelectorAll('.community-element:not(.general-community)');
    allCommunities.forEach(community => {
        community.querySelector('.bi-pencil-square').classList.remove('hide');
    community.querySelector('.bi-trash').classList.remove('hide');
});
}

/**
 * Function to edit all news list
 */
function makeEditableNews() {
    removeNewsEditDetailsBtn.classList.remove('hide');
    editNewsBtn.classList.add('hide');
    const allNewsItems = newsContainer.querySelectorAll('li.news-content .news-options');
    allNewsItems.forEach(icon => icon.classList.remove('hide'));
}


/**
 * Function to remove the editable news
 */
function removeEditableNews() {
    removeNewsEditDetailsBtn.classList.add('hide');
    editNewsBtn.classList.remove('hide');
    const allNewsItems = newsContainer.querySelectorAll('li.news-content .news-options');
    allNewsItems.forEach(icon => icon.classList.add('hide'));
}


/**
 * Create an arrow function that will be called when an image is selected.
 */
const previewCommunityImages = (file) => {
    const fr = new FileReader();
fr.onload = () => {
    const newElement = document.createElement('div');
newElement.className = "previewImage";
newElement.dataset.imageUploadIndex = imageIndexForUploadInCommunity;
newElement.innerHTML = `            
    <img src="${fr.result}" class="mb-2" alt="${file.name}" defer/>            
`;
document.querySelector(`.image-preview-grid-${communityPreviewContainerCount}`).append(newElement);
imageIndexForUploadInCommunity++;
communityPreviewContainerCount++;
if (communityPreviewContainerCount > 4) {
    communityPreviewContainerCount = 1;
}
};
fr.readAsDataURL(file);
};

/**
 * Function to create community event images
 */
function previewCommunityEventImages(file) {
    const fr = new FileReader();
    fr.onload = () => {
        const newElement = document.createElement('div');
    newElement.className = "previewImage";
    newElement.dataset.imageUploadIndex = imageEventIndexForUploadInCommunity;
    newElement.innerHTML = `            
        <img src="${fr.result}" class="mb-2" alt="${file.name}" defer/>            
    `;
    document.querySelector(`.image-event-preview-grid-${communityEventPreviewContainerCount}`).append(newElement);
imageEventIndexForUploadInCommunity++;
communityEventPreviewContainerCount++;
if (communityEventPreviewContainerCount > 4) {
    communityEventPreviewContainerCount = 1;
}
};
fr.readAsDataURL(file);
}



/**
 * Convert time to a valid ISO String
 */
//window.toIsoString = (date) => {
//    console.log(fnBrowserDetect())
//    var tzo = -date.getTimezoneOffset(),
//        dif = tzo >= 0 ? '+' : '-',
//        pad = function (num) {
//            return (num < 10 ? '0' : '') + num;
//        };

//    return date.getFullYear() +
//        '-' + pad(date.getMonth() + 1) +
//        '-' + pad(date.getDate()) +
//        'T' + pad(date.getHours()) +
//        ':' + pad(date.getMinutes()) +
//        ':' + pad(date.getSeconds()) +
//        dif + pad(Math.floor(Math.abs(tzo) / 60)) +
//        ':' + pad(Math.abs(tzo) % 60);
//}

/**
 * Preview Community Video
 */
function previewCommunityVideo() {
    let file = event.target.files[0];
    let blobURL = URL.createObjectURL(file);
    communityVideoPlayer.src = blobURL;
}


/**
 * Preview Community Event Video Image
 */
function previewCommunityEventVideo1() {
    let file = event.target.files[0];
    let blobURL = URL.createObjectURL(file);
    communityEventVideoPlayer.src = blobURL;
}

/**
 * Preview Community Event Video Image
 */
function previewCommunityPostVideo(player) {
    let file = event.target.files[0];
    let blobURL = URL.createObjectURL(file);
    communityEventVideoPlayer.src = blobURL;
}


/**
 * Calculate the time elapsed since present date
 * @param {Calculate the time elapsed since present date} date 
 */
function timeSince(date) {
    var seconds = Math.floor((new Date() - new Date(date)) / 1000);
    var interval = seconds / 31536000;
    if (interval > 1) {
        return Math.floor(interval) + "y ago";
    }
    interval = seconds / 2592000;
    if (interval > 1) {
        return Math.floor(interval) + "m ago";
    }
    interval = seconds / 86400;
    if (interval > 1) {
        return Math.floor(interval) + "d ago";
    }
    interval = seconds / 3600;
    if (interval > 1) {
        return Math.floor(interval) + "h ago";
    }
    interval = seconds / 60;
    if (interval > 1) {
        return Math.floor(interval) + "m ago";
    }
    return Math.floor(seconds) + "s ago";
}


/**
 * teuncate funciton
 */
function toggleText(element, length) {
    const textContainer = element.querySelector('span.text');

    const text = textContainer.textContent;
    const showMore = document.createElement('span');
    showMore.textContent = '(read more)';
    showMore.className = 'blue-icon';
    const showLess = document.createElement('span');
    showLess.textContent = '(show less)';
    showLess.className = 'blue-icon hide';

    showMore.addEventListener('click', () => {
        showMore.classList.add('hide');
    showLess.classList.remove('hide');
    textContainer.textContent = text;
});

showLess.addEventListener('click', () => {
    showMore.classList.remove('hide');
showLess.classList.add('hide');
textContainer.textContent = `${text.substring(0, length)} ...`;
});

if (text.length > length) {
    textContainer.innerHTML = `${text.substring(0, length)} ...`;
    element.appendChild(showMore);
    element.appendChild(showLess);
}
else {
    element.innerHTML = text;
}
}


/**
 * Function top format bytes
 */
function formatBytes(bytes, decimals = 2) {
    if (!+bytes) return '0 Bytes'

    const k = 1024
    const dm = decimals < 0 ? 0 : decimals
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB']

    const i = Math.floor(Math.log(bytes) / Math.log(k))

    return `${parseFloat((bytes / Math.pow(k, i)).toFixed(dm))} ${sizes[i]}`
}

    /**
     * Convert time to a valid ISO String
     */
    window.toIsoString = (date) => {
        var tzo = -date.getTimezoneOffset(),
        dif = tzo >= 0 ? '+' : '-',
        pad = function (num) {
            return (num < 10 ? '0' : '') + num;
        };

    return date.getFullYear() +
        '-' + pad(date.getMonth() + 1) +
        '-' + pad(date.getDate()) +
        'T' + pad(date.getHours()) +
        ':' + pad(date.getMinutes()) +
        ':' + pad(date.getSeconds()) +
        dif + pad(Math.floor(Math.abs(tzo) / 60)) +
        ':' + pad(Math.abs(tzo) % 60);
}

})

