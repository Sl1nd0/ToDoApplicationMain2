function connect(userName, token, path) {
    location.href = "/Account/Connect?userName=" + userName + "&token=" + token + "&path=" + path;
}
function logout() {
    //alert("Loging Laout out")
    location.href = "/Account/Logout";
}

//alert("window.location");
//alert(window.location);

function getMainUrl() {
    //alert("getMainUrl");
    var url = window.location.href;
    //Get the raw URL
    if (url.indexOf("Account") > -1) {
        url.substr(0, url.indexOf("Account") - 1);
        //alert("newUrl");
        //alert(url);
        return url;
    }
    return url;
}

function createAccount(path) {
    ////alert(path);
    //alert("createAccount " + getMainUrl() + path);
    location.href = getMainUrl() + path;
}

function loginAccount(path) {
    ////alert(path);
    ////alert( getMainUrl() + path);
    location.href = getMainUrl() + path;
}

//Route thing in the url
// var url = window.location.href;
// //Get the raw URL
// if (url.indexOf("Account") > -1) {
//     var newUrl = url.substr(0, url.indexOf("Account")-1);
//     //alert(newUrl);
// }

// //alert(url);

// //alert(url.indexOf("Account"))

navBarSort();

function navBarSort() {

    if (url.indexOf("Create") > -1) {
        // document.getElementById("createaccount").disabled = true;
        var tagName = document.getElementById("createaccount");
        tagName.style.backgroundcolor = "grey";
        clickDisable(tagName);
    }

    if (url.indexOf("Login") > -1) {
        // document.getElementById("createaccount").disabled = true;
        var tagName = document.getElementById("loginaccount");
        tagName.style.backgroundcolor = "grey";
        clickDisable(tagName);
    }

}

function clickDisable(elementName) {
    elementName.addEventListener("click", function (event) {
        event.preventDefault(); // Prevents the link from navigating to its href
        // Optionally, you can add visual cues to indicate the link is "disabled"
        // For example, change its styling or add a class.
        elementName.style.cursor = "not-allowed";
        elementName.style.opacity = "0.5";
    });
}

// document.getElementById("createaccount").onclick = function() {
//

// }

function editProfileDetails(username, token) {
    //alert("Layout");
    //alert(username);
    //alert(token);

    location.href = "/Account/Edit?userName=" + username + "&token=" + token;
    alert(username);
}