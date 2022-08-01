// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



let Notifications = document.querySelector('.Notification-container');
let Logout2 = document.querySelector('.Logout-container');
let Notifications2 = document.querySelector('.Notification-container');
let searchBar2 = document.querySelector('.Search-container');

//post img / video
function showPostDiv() {
    var postbox = document.getElementById('postBox');
    if (postbox.style.display === "block") {
        postbox.style.display = "none";
    }
    else {
        postbox.style.display = "block";
    }
}


var input = document.getElementById('imgInput');
var uploadedimage = "";

if (input != null) {
    input.addEventListener("change", function () {
        const reader = new FileReader();
        reader.addEventListener("load", () => {
            uploadedimage = reader.result;
            document.getElementById('output').style.content = `url(${uploadedimage})`;
        });
        reader.readAsDataURL(this.files[0]);
    })
}


//darkmode
var light = document.getElementById('light');
var dark = document.getElementById('dark');
if (light != null) {
    light.addEventListener("click", function () {
        document.body.classList.remove('darkmode');
    })
}
if (dark != null) {
    dark.addEventListener("click", function () {
        document.body.classList.add('darkmode');
    })
}




// var light2 = document.getElementById('light2');
// var dark2 = document.getElementById('dark2');
// dark.addEventListener("click",function(){
//     document.body.classList.add('darkmode');
// })
// light.addEventListener("click",function(){
//     document.body.classList.remove('darkmode');
// })


//cover

// var inputCover = document.getElementById('coverPhoto');
// var coverImage = "";
// inputCover.addEventListener("change",function(){
//     const reader = new FileReader();
//     reader.addEventListener("load",()=>{
//         coverImage = reader.result;
//         document.getElementById('cover').style.content = `url(${coverImage})`;
//         // document.getElementById('coverGred').style.backgroundColor = "black";
//     });
//     reader.readAsDataURL(this.files[0]);
// })

//profile Picture
// var inputProfile = document.getElementById('profilePic');
// var profileImage = "";
// inputProfile.addEventListener("change",function(){
//     const reader = new FileReader();
//     reader.addEventListener("load",()=>{
//         profileImage = reader.result;
//         document.getElementById('profile').style.content = `url(${profileImage})`;
//         document.getElementById('coverGred').style.backgroundColor = "black";
//     });
//     reader.readAsDataURL(this.files[0]);
// })




//show comments
function showComments(x) {
    id = "comments+" + x
    var comments = document.getElementById(id);
    if (comments.style.display === "none") {
        comments.style.display = "block";
    }
    else {
        comments.style.display = "none";
    }
}



//show Likes
function showLikes(x) {
    id = "likes+" + x
    var comments = document.getElementById(id);
    if (comments.style.display === "none") {
        comments.style.display = "grid";
    }
    else {
        comments.style.display = "none";
    }
}

function closeLikes(x) {
    id = "likes+" + x
    var comments = document.getElementById(id);
    if (comments.style.display === "none") {
        comments.style.display = "grid";
    }
    else {
        comments.style.display = "none";
    }
}

//overlay img
function showOverlay() {
    var imgUrl = document.getElementsByClassName('ImgUrl');
    var overlay = document.getElementById('parentOverlayDiv');

    for (let i = 0; i < imgUrl.length; i++) {
        imgUrl[i].addEventListener('click', function () {
            overlay.style.display = "block";
            overlay.innerHTML = `<div class="childOverlay" >
                <i class="fa-solid fa-xmark" onclick="closeOverlay()"></i>
                <img src="${imgUrl[i].src}" style="max-width:50%;"   alt=""><div>`
        })
    }


}
function closeOverlay() {
    var overlay = document.getElementById('parentOverlayDiv');
    overlay.style.display = "none";
}



//visibility
var dropdownMenu2 = document.getElementById('dropdownMenu2')
function putOnlyMe() {
    dropdownMenu2.innerHTML = `<i class="fa-solid fa-lock "></i> Only Me`
}
function putPublic() {
    dropdownMenu2.innerHTML = `<i class="fa-solid fa-earth-americas"></i> Public`
}
function putFriends() {
    dropdownMenu2.innerHTML = `<i class="fa-solid fa-user-group"></i> Friends`
}


//ram
if (document.querySelector('#bell') != null) {
    document.querySelector('#bell').onclick = () => {
        Notifications.classList.toggle('active');
        ArrowBar.classList.remove('active');
        searchBar.classList.remove('active');
    }
}

function showNot() {

    Notifications2.classList.toggle('active');
    Logout2.classList.remove('active');
    searchBar.classList.remove('active');
}

function showLogout() {

    Logout2.classList.toggle('active');
    Notifications2.classList.remove('active');
    searchBar.classList.remove('active');
}

function showSearch() {
    searchBar2.classList.toggle('active');
    Notifications2.classList.remove('active');
    Logout2.classList.remove('active');
}


let ArrowBar = document.querySelector('.Logout-container');

if (document.querySelector('#arrow') != null) {
    document.querySelector('#arrow').onclick = () => {
        ArrowBar.classList.toggle('active');
        Notifications.classList.remove('active');
        searchBar.classList.remove('active');


    }
}

let searchBar = document.querySelector('.Search-container');

if (document.querySelector('#searching')) {
    document.querySelector('#searching').onclick = () => {
        searchBar.classList.toggle('active');
        Notifications.classList.remove('active');
        ArrowBar.classList.remove('active');


    }
}


// let like = document.getElementById('like')
// function changeLikeColor(){
//     like.classList.toggle('activeLike');


// }

//let likebtn = document.querySelectorAll('.likes');
//if (likebtn != null) {
//    likebtn.forEach((btn) => {
//        btn.addEventListener('click', (e) => {
//            // console.log(e.currentTarget);
//            e.currentTarget.classList.toggle('activeLike');
//            e.currentTarget.nextElementSibling.classList.remove('activeDislike');
//        })
//    })
//}



//let Dislikebtn = document.querySelectorAll('.dislikes');

//if (Dislikebtn != null) {
//    Dislikebtn.forEach((btn) => {
//        btn.addEventListener('click', (e) => {
//            console.log(e.currentTarget.previousElementSibling);
//            console.log(e.currentTarget.nextElementSibling);
//            e.currentTarget.classList.toggle('activeDislike');
//            e.currentTarget.previousElementSibling.classList.remove('activeLike');
//        })
//    })
//}












//bio buttons 
// function showBioDiv(){
//     if(bio.style.display === "none"){
//         bio.style.display = "block";
//     }
//     else{
//         bio.style.display = "none";
//     }
// }
// function printBio(){
//     var bioDetails = document.getElementById('bioDetails').value;
//     var bioprint = document.getElementById('bioPrinted');
//     var btnBio2 = document.getElementById('btnBio2');
//     var btnBio = document.getElementById('btnBio');
//     bioprint.innerHTML = `<h6>${bioDetails}</h6>`;
//     btnBio.style.display = "none";
//     bio.style.display = "none";
//     btnBio2.style.display = "block";
//     if(bioDetails === ""){
//         btnBio.style.display = "block";
//         btnBio2.style.display = "none";
//     }
// }

// function cancelBio(){
//     bio.style.display = "none";
// }

// //Edit Details button
// function showDetailsDiv(){
//     var detailsDiv = document.getElementById('detailsDiv');
//     if(detailsDiv.style.display === "none"){
//         detailsDiv.style.display = "block";
//     }
//     else{
//         detailsDiv.style.display = "none";
//     }
// }

// function showWork(){
//     var work = document.getElementById('work');
//     if(work.style.display === "none"){
//         work.style.display = "block"
//     }
//     else{
//         work.style.display = "none"
//     }
// }