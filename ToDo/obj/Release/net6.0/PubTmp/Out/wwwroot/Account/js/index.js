//alert("account Index");

//alert(document.getElementById("userdetail").innerHTML)

//alert("account Index");

var myuser = "";
function setUser(user) {

    //alert(user);
    myuser = user;

    //document.getElementById("myuser").value = user;
    ////alert("document.getElementById(myuser).value")
    ////alert(document.getElementById("myuser").value)

    //alert(document.getElementById("userdetail").value)

    //alert(document.getElementById("userdetail").innerHTML)

    ////alert(document.getElementById("myuser").value);
}
//document.getElementById("createtask").onclick = function () {
//    location.href = "/Todos/Create?email=" + myuser + "&username=" + myuser;
//}

function connect(userName, token) {
    if (userName.length != 0) {
        var path = "account"
        location.href = "/Account/Connect?userName=" + userName + "&token=" + token + "&path=" + path;
    }
}

function viewTasks(userName, token, connectionuserID, userID, taskEmail) {
    //alert("connectionID")
    //alert(userName)
    //alert(token)
    //alert(connectionuserID)
    //alert(userID)

    location.href = "/Account/ViewConnectionTasks?email=" + userName + "&username=" + userName + "&token=" + token + "&connectionID=" + connectionuserID + "&userID=" + userID + "&taskEmail=" + taskEmail

    let result = '';
    var blob = new Blob(['Hello'], {
        type: "text/csv;charset=utf-8;"
    });
    saveAs(blob, "test.csv")

}

function goBackToPrev(email, password, userID) {
    //alert(email);
    //alert(password);
    //alert(userID);
    var path = "account"
    location.href = "/Account/ViewConnections?userName=" + email + "&token=" + password + "&path=" + path;
}

function viewConnections(userName, token) {
    //alert(userName)
    //alert(token)
    if (userName.length != 0) {
        var path = "account"
        location.href = "/Account/ViewConnections?userName=" + userName + "&token=" + token + "&path=" + path;
    }
}

function deleteConnection(deleteConnectionID, userName, token, userID) {
    //alert(deleteConnectionID)
    //alert(userName)
    //alert(token)
    //alert(userID)
    var account = "account";
    //if (userName.length != 0) {
    //    var path = "account"

    //location.href = "Account/ViewConnections?userName=" + username + "&token=" + token + "&path=" + account

    //location.href = "/Account/Index?email=" + userName + "&username=" + userName + "&token=" + token + "&userID=" + userID;


    fetch('/Account/DeleteConnection', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: deleteConnectionID
        })
    })
        .then(response => {

            if (response.ok) {
                return response.json();
            } else {
                response.text().then(text => alert("Error: " + text));
            }
        }).then(data => {
            //console.log('Parsed JSON:', data.success); // This contains the actual data

            //alert(data.success)
            console.log(data)
            console.log(JSON.stringify(data))
            console.log(myuser)

            if (data.success) {
                //Refresh
                //location.href = "";
                alert("Connection has been deleted");

                location.href = "/Account/ViewConnections?userName=" + userName + "&token=" + token + "&path=" + account
                //location.href = "/Account/Edit?userName=" + username
            } else {
                if (data.error) {
                    console.error('Wow yoh');
                    alert("Error: " + data.error);
                }
            }
        })
        .catch(error => {
            console.error('Wow yoh');
            console.error('Error:', error);
            alert("An error occurred while loging into the account.");

        });
}

function addConnection(connectionId, userId, userName, token) {

    var path = "account";
    //alert(connectionId)
    //alert(userId)
    //alert(userName)
    //alert(token)

    //location.href = "/Account/Connect?userName=" + userName + "&token=" + token + "&path=" + path;

    fetch('/Account/Connect', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserID: userId,
            ConnectionUserID: connectionId,
        })
    })
        .then(response => {

            if (response.ok) {
                return response.json();
            } else {
                response.text().then(text => alert("Error: " + text));
            }
        }).then(data => {
            //console.log('Parsed JSON:', data.success); // This contains the actual data

            //alert(data.success)
            console.log(data)
            console.log(JSON.stringify(data))
            console.log(myuser)

            if (data.success) {
                //Refresh
                //location.href = "";
                alert("Connection has been added");

                location.href = "/Account/Connect?userName=" + userName + "&token=" + token + "&path=" + path;
                //location.href = "/Account/Edit?userName=" + username
            } else {
                if (data.error) {
                    console.error('Wow yoh');
                    alert("Error: " + data.error);
                }
            }
        })
        .catch(error => {
            console.error('Wow yoh');
            console.error('Error:', error);
            alert("An error occurred while loging into the account.");

        });
}

function createATask(myuser, token, userID) {

    //alert("userID");
    //alert(userID);
    //alert("userID");
    //alert("userID");

    location.href = "/Todos/Create?email=" + myuser + "&username=" + myuser + "&token=" + token + "&userID=" + userID;

    //fetch('/ToDos/CreateTask', {
    //    method: 'POST',
    //    headers: {
    //        'Content-Type': 'application/json'
    //    },
    //    body: JSON.stringify({
    //        UserName: myuser,
    //        Email: myuser,
    //        Password: "0"
    //    })
    //})
    //    .then(response => {

    //        if (response.ok) {
    //            return response.json();
    //        } else {
    //            response.text().then(text => //alert("Error: " + text));
    //        }
    //    })
    //    .then(data => {
    //        //console.log('Parsed JSON:', data.success); // This contains the actual data
    //        ////alert("It's OK");

    //        console.log(JSON.stringify(data))
    //        //console.log(myuser)

    //        if (data != null && data.getUserRequest.userName != null) {

    //            location.href = "/ToDos/Create?email=" + data.getUserRequest.userName + "&username=" + data.getUserRequest.userName;

    //        } else {
    //            if (data.error) {
    //                console.error('Wow yoh');
    //                //alert("Error: " + data.error);
    //            }
    //        }
    //    })
    //    .catch(error => {
    //        console.error('Wow yoh');
    //        console.error('Error:', error);
    //        //alert("An error occurred while loging into the account.");
    //    });
}

/*
function goBackToMain(myuser, password) {

    //alert("Comment");
    //alert(btoa(password));
    //alert(myuser);

    location.href = "/Account/Index?email=" + myuser + "&username=" + myuser + "&token=" + password;
}
*/


function goBackToMainMenu(myuser, token, userID, connectionID, taskEmail) {

    //alert("Back");
    //alert(token);
    //alert(userID);
    //alert("Back");

    location.href = "/Account/ViewConnectionTasks?email=" + myuser + "&username=" + myuser + "&token=MTExMQ" + "&connectionID=" + connectionID + "&userID=" + userID + "&taskEmail=" + taskEmail

}

function goBackToMain(myuser, token, userID) {

    //alert("Back");
    //alert(token);
    //alert(userID);
    //alert("Back");

    location.href = "/Account/Index?email=" + myuser + "&username=" + myuser + "&token=" + token + "&userID=" + userID;
}


function goBackToMainView() {

    //alert("Comment");
    //alert(document.getElementById("email").value);
    //alert(document.getElementById("password").value); 

    //location.href = "/Account/Index?email=" + myuser + "&username=" + myuser + "&token=" + password;
}



function commentTask(myuser, todo, title, token, toDoID, userID) {

    //alert("Comment");
    //alert(todo + title);
    //alert("Comment");
    //alert(toDoID);
    //alert(token);
    //alert(userID);
    //alert("Comment");

    location.href = "/Todos/Comment?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID;
}

function commentTaskNew(myuser, todo, title, token, toDoID, userID, taskView, connectionID, taskEmail) {

    //alert("Comment");
    //alert(taskView);
    //alert("Comment");
    //alert(toDoID);
    //alert(token);
    //alert(userID);
    //alert("Comment");

    location.href = "/Todos/CommentNew?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID + "&taskView=" + taskView + "&connectionID=" + connectionID + "&taskEmail=" + taskEmail;
}

function commentTask(myuser, todo, title, token, toDoID, userID) {

    //alert("Comment");
    //alert(todo + title);
    //alert("Comment");
    //alert(toDoID);
    //alert(token);
    //alert(userID);
    //alert("Comment");

    location.href = "/Todos/Comment?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID;
}

/*
function editTask(myuser, todo, title) {

    //alert("editTask")

    //alert("Edit " + todo + " " + title);
    //alert("Edit " + todo + " " + title);

    location.href = "/Todos/Edit?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title);
}*/

function goBackToConnections(email, token, userId) {
    location.href = "/Account/ViewConnections?userName=" + email + "&token=" + token + "&path=" + "account"
}

function editTask(myuser, todo, title, todoID, token, userID) {

    //alert("Edit");
    //alert(todoID);
    //alert("todoID");

    location.href = "/Todos/Edit?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&todoID=" + todoID + "&token=" + token + "&userID=" + userID;
}




function deleteTask(myuser, todo, title, todoID, token, userID) {
    //alert("delete M");
    //alert(todo + title);
    //alert( todoID)

    location.href = "/Todos/Delete?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&todoID=" + todoID + "&token=" + token + "&userID=" + userID;
}
function logout() {
    //alert("Loging inex acc out")
    location.href = "/Account/Logout";
}

function editProfileDetails(username, token) {
    //alert("Account");
    //alert(username);
    //alert("Account");
    //alert(username);
    // alert(token);
    /*
     fetch(`/Account/Edit?commentID=${encodeURIComponent(commentID)}&token=token`, {
         method: 'GET',
         headers: {
             'Accept': 'application/json'
         }
     })
         .then(response => {
             if (response.ok) {
                 return response.json();
             } else {
                 return response.text().then(text => {
                     alert("Error: " + text);
                     throw new Error(text);
                 });
             }
         })
         .then(data => {
             if (data.success) {
                 window.location.href = "/";
             } else {
                 console.error("Error in response");
             }
         })
         .catch(error => {
             console.error("Hey", error);
         });*/

    location.href = "/Account/Edit?userName=" + username + "&token=" + token;
}



function saveEditUserProfile(username, token, userId) {
    //alert("ToDos");
    //alert(username);
    //alert(token);
    //alert(userId);

    fetch('/Account/Edit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Name: document.getElementById("name").value,
            UserId: userId,
            Surname: document.getElementById("surname").value,
            UserName: document.getElementById("email").value,
            Email: document.getElementById("email").value,
            Identifier: document.getElementById("identifier").value,
            DateOfBirth: document.getElementById("dob").value,
            CellNumber: document.getElementById("cellnumber").value,
        })
    })
        .then(response => {

            if (response.ok) {
                return response.json();
            } else {
                response.text().then(text => alert("Error: " + text));
            }
        }).then(data => {
            //console.log('Parsed JSON:', data.success); // This contains the actual data

            //alert(data.success)
            console.log(data)
            console.log(JSON.stringify(data))
            console.log(myuser)

            if (data.getUserRequest != null) {
                //Refresh
                //location.href = "";
                alert("Your account has been updated successfully!");
                location.href = "/Account/Index?email=" + username + "&username=" + username + "&token=" + token + "&userID=" + userId;

                //location.href = "/Account/Edit?userName=" + username
            } else {
                if (data.error) {
                    console.error('Wow yoh');
                    alert("Error: " + data.error);
                }
            }
        })
        .catch(error => {
            console.error('Wow yoh');
            console.error('Error:', error);
            alert("An error occurred while loging into the account.");

        });
}

function downloadCSV(email) {
    
    fetch('/Account/DownloadReport', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserName: email,
            UserID: 1,
            Page: 1,
            PageSize: 7,
            Search: ""
        })
    })
        .then(response => {

            if (response.ok) {
                return response.json();
            } else {
                response.text().then(text => alert("Error: " + text));
            }
        }).then(data => {
            //alert(data.success)

            console.log(data)
            console.log(JSON.stringify(data))
            //console.log(myuser)

            if (data != null) {

                //Set data and headers
                var headerData = "Id, UsersID, ToDo, ToDoTitle\n"

                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        headerData += data[i].id + "," + data[i].usersID + ",\"" + data[i].toDo + "\",\"" + data[i].toDoTitle + "\"\n"
                    }
                } else {
                    headerData = "No tasks to show for this user"
                    alert(headerData);
                }

                createACSV(headerData);

                alert("Report downloaded successfully!");
                
            } else {
                if (data.error) {
                    console.error('Wow yoh');
                    alert("Error: " + data.error);
                }
            }
        })
        .catch(error => {
            console.error('Wow yoh');
            console.error('Error:', error);
            alert("An error occurred while downloading a task report.");

        });
}

function createACSV(fileContent) {

    const csvContent = fileContent;

    const blob = new Blob([csvContent], { type: "text/csv;charset=utf-8;" });
    const url = URL.createObjectURL(blob);

    const link = document.createElement("a");
    link.setAttribute("href", url);
    link.setAttribute("download", "data.csv");
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}