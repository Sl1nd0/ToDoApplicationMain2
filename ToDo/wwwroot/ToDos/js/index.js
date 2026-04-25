//alert("account Index");

var myuser = "";
function setUser(user) {
    ////alert(user);
    myuser = user;
    document.getElementById("myuser").value = user;
    ////alert(document.getElementById("myuser").value);
}

//document.getElementById("createtask").onclick = function () {
//    location.href = "/Todos/Create?email=" + myuser + "&username=" + myuser;
//}


function addTaskComment(myuser, token, title, todo, todoID, userID, connectionID, taskEmail) {

    //alert(todoID)
    //alert(myuser)
    //alert(token)
    //alert(title)
    //alert(todo)
    //alert(todoID)
    //alert(userID)
    //alert(connectionID)
    //alert(taskEmail)

    //alert("Edit");

    fetch('/ToDos/Comment', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserID: userID,
            ToDoID: todoID,
            Comment: document.getElementById("comment").value,
            UserName: myuser,
            ToDo: todo,
            ToDoTitle: title
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
            console.log(data.success)
            console.log(JSON.stringify(data))
            console.log(myuser)

            if (data.success) {

                //location.href = "/Account/Index?email=" + myuser + "&username=" + myuser + "&token=" + token + "&userID=" + userID;
                console.log("toDoID");

                console.log(todoID.toString());

                console.log("toDoID");

                var taskView = true;

                location.href = "/Todos/CommentNew?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
                    + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + todoID.toString() + "&userID=" + userID + "&taskView=" + taskView.toString() + "&connectionID=" + connectionID + "&taskEmail=" + taskEmail;

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

function goBackToTasks(userName, token, connectionuserID, userID, taskEmail) {

    //alert(userName)
    //alert(token)
    //alert(connectionuserID)
    //alert(userID)
    //alert(taskEmail)

    location.href = "/Account/ViewConnectionTasks?email=" + userName + "&username=" + userName + "&token=" + token + "&connectionID=" + connectionuserID + "&userID=" + userID + "&taskEmail=" + taskEmail
}

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


//function goBackToMainView() {

//    alert("Comment");
//    alert(document.getElementById("email").value);
//    alert(document.getElementById("password").value);

//    //location.href = "/Account/Index?email=" + myuser + "&username=" + myuser + "&token=" + password;
//}

function commentTask(myuser, todo, title, token, toDoID, userID) {

    //alert("Comment");
    //alert(toDoID);
    //alert(token);
    //alert(userID);
    //alert("Comment");

    location.href = "/Todos/Comment?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID;
}

function deleteTask(myuser, todo, title, todoID) {
    //alert("delete MM");
    //alert(todo + title);
    //alert("  " + todoID)

    location.href = "/Todos/Delete?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&todoID=" + todoID;
}

function editTask(myuser, todo, title, todoID) {

    //alert("Edit");
    //alert(todoID);
    //alert("todoID");

    location.href = "/Todos/Edit?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&todoID=" + todoID;
}

function createATask(myuser, token, userID) {
    //alert('createATask')
    //alert(myuser)
    var task = document.getElementById('task').value;
    var taskTitle = document.getElementById('todoTitle').value;

    /*  
        public int UsersID { get; set; }
        public string UserName { get; set; }
        public string ToDo { get; set; }
        public string Title { get; set; }
    */

    fetch('/ToDos/Create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserName: myuser,
            Email: myuser,
            Password: token,
            toDo: task,
            usersid: 0,
            title: taskTitle
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
            console.log(data.success)
            console.log(JSON.stringify(data))
            console.log(myuser)

            if (data.success) {

                location.href = "/Account/Index?email=" + myuser + "&username=" + myuser + "&token=" + token + "&userID=" + userID;
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

function saveChangesEdit(myuser, todo, title, toDoID, token, userID) {

    //alert("Saving ")
    //alert(todo)
    //alert(toDoID)
    //alert(title)

    fetch('/ToDos/Edit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserName: encodeURI(myuser),
            toDo: encodeURI(todo),
            ToDoID: toDoID,
            toDoTitle: encodeURI(title)
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
            console.log(data.success)
            console.log(JSON.stringify(data))
            console.log(myuser)

            if (data.success) {

                location.href = "/Account/Index?email=" + myuser + "&username=" + myuser + "&token=" + token + "&userID=" + userID;
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

function saveChangesDelete(myuser, todo, title, toDoID, token, userID) {

    //alert("Saving saveChangesDelete")

    //alert(todo)
    //alert(toDoID)
    //alert(title)

    fetch('/ToDos/Delete', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserName: encodeURI(myuser),
            toDo: encodeURI(todo),
            ToDoID: toDoID,
            toDoTitle: encodeURI(title)
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
            console.log(data.success)
            console.log(JSON.stringify(data))
            console.log(myuser)

            if (data.success) {

                location.href = "/Account/Index?email=" + myuser + "&username=" + myuser + "&token=" + token + "&userID=" + userID;
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


function addComment(myuser, token, title, todo, todoID, userID) {
    //alert("Edit");
    //alert(todoID);
    //alert("todoID");

    //alert("Comment");

    //alert(myuser);
    //alert(todoID);
    //alert(userID);

    //alert(token);

    //alert(document.getElementById("comment").value);

    //Post the comment

    fetch('/ToDos/Comment', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserID: userID,
            ToDoID: todoID,
            Comment: document.getElementById("comment").value,
            UserName: myuser,
            ToDo: todo,
            ToDoTitle: title
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
            console.log(data.success)
            console.log(JSON.stringify(data))
            console.log(myuser)

            if (data.success) {

                location.href = "/Account/Index?email=" + myuser + "&username=" + myuser + "&token=" + token + "&userID=" + userID;
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

function deleteComment(commentID) {

    //alert("commentID ");
    //alert(commentID);
    //alert("commentID ");

    //location.href = "/Todos/DeleteComment?id=" + commentID;
    //Post the comment

    fetch('/ToDos/DeleteComment', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            CommentID: commentID
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
            console.log(data.success)
            console.log(JSON.stringify(data))
            console.log(myuser)

            if (data.success) {
                //Refresh
                location.href = "";
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

function escapeHtml(unsafe) {
    alert(unsafe);
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}
function editComment(comment, commentID, email, token, userId, todo, todoId, todoTitle) {

    //alert("Edit Comment");
    //alert(commentID);

    var newcomment = comment;

    //alert(newcomment.indexOf("q2"));


    while (newcomment.indexOf("q1") > -1) {
        newcomment = newcomment.replace("q1", "'");
    }

    while (newcomment.indexOf("q2") > -1) {
        newcomment = newcomment.replace("q2", "\"");
    }

    //alert("newcomment");
    //alert(newcomment);

    //alert(comment);
    //alert(decodeURI(email));
    //alert(decodeURI(token));
    //alert(userId);
    //alert(decodeURI(todo));
    //alert(todoId);
    //alert(decodeURI(todoTitle));
    //alert("editComment");

    var check = "/ToDos/EditComment?comment=" + newcomment + "&commentID=" + commentID + "&username=" + email + "&todoTitle=" + todoTitle
        + "&todo=" + todo + "&token=" + token + "&toDoID=" + todoId + "&userID=" + userId;

    console.log(check);

    location.href = check;
    //string comment, string username, string todoTitle, string todo, string token, int toDoID, int userID

    //var check = "/Todos/Comment?email=" + encodeURI(email) + "&username=" + encodeURI(email)
    //    + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(todoTitle) + "&token=" + token + "&toDoID=" + todoId + "&userID=" + userId;

}

function editCommentNew(comment, commentID, email, token, userId, todo, todoId, todoTitle, taskEmail, connectionID) {

    //alert("Edit Comment");
    //alert(commentID);

    //alert(comment);
    //alert(taskEmail);
    //alert(connectionID);

    var newcomment = comment;

    //alert(newcomment.indexOf("q2"));


    while (newcomment.indexOf("q1") > -1) {
        newcomment = newcomment.replace("q1", "'");
    }

    while (newcomment.indexOf("q2") > -1) {
        newcomment = newcomment.replace("q2", "\"");
    }

    //alert("newcomment");
    //alert(newcomment);

    //alert(comment);
    //alert(decodeURI(email));
    //alert(decodeURI(token));
    //alert(userId);
    //alert(decodeURI(todo));
    //alert(todoId);
    //alert(decodeURI(todoTitle));
    //alert("editComment");

    var check = "/ToDos/EditCommentANew?comment=" + newcomment + "&commentID=" + commentID + "&username=" + email + "&todoTitle=" + todoTitle
        + "&todo=" + todo + "&token=" + token + "&toDoID=" + todoId + "&userID=" + userId + "&taskEmail=" + taskEmail + "&connectionID=" + connectionID + "&taskView=" + true;


    console.log(check);

    location.href = check;
    //string comment, string username, string todoTitle, string todo, string token, int toDoID, int userID

    //var check = "/Todos/Comment?email=" + encodeURI(email) + "&username=" + encodeURI(email)
    //    + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(todoTitle) + "&token=" + token + "&toDoID=" + todoId + "&userID=" + userId;

}

function goBackToComment(myuser, todo, title, token, toDoID, userID) {

    //alert("Comment");
    //alert(myuser);
    //alert(todo);
    //alert(title);
    //alert(token);
    //alert(toDoID);
    //alert(userID);
    //alert("Comment");

    location.href = "/Todos/Comment?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID;
}

function goBackToCommentNew(myuser, todo, title, token, toDoID, userID, taskView, connectionID, taskEmail) {

    //alert("Comment");
    //alert(myuser);
    //alert(todo);
    //alert(title);
    //alert(token);
    //alert(toDoID);
    //alert(userID);
    //alert("Comment");

    location.href = "/Todos/Comment?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
        + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID
        + "&taskView=" + taskView.toString() + "&connectionID=" + connectionID + "&taskEmail=" + taskEmail;
}

function saveEditCommentNew(commentID, myuser, todo, title, token, toDoID, userID, comment, taskView, connectionID, taskEmail) {

    //alert(taskView);
    //alert(connectionID);
    //alert(taskEmail);

    // alert("saveEditComment Comment");
    //alert(commentID);
    //alert(myuser);
    //alert(todo);
    //alert(title);
    //alert(token);
    //alert(toDoID);
    //alert(userID);
    //alert(comment);
    //alert("saveEditComment Comment");

    //Comment fetch
    fetch('/ToDos/EditComment', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            CommentID: commentID,
            Comment: comment,
            Password: token,
            Email: myuser
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

            if (data.success) { //???
                //Refresh

                var check = "/Todos/CommentNew?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
                    + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID
                    + "&taskView=" + taskView.toString() + "&connectionID=" + connectionID + "&taskEmail=" + taskEmail;

                console.log(check)

                location.href = check;

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


    //Home
    //location.href = "/Todos/Comment?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
    //  + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID;
}

function saveEditComment(commentID, myuser, todo, title, token, toDoID, userID, comment) {

    //alert(comment);
    // alert("saveEditComment Comment");
    //alert(commentID);
    //alert(myuser);
    //alert(todo);
    //alert(title);
    //alert(token);
    //alert(toDoID);
    //alert(userID);
    //alert(comment);
    //alert("saveEditComment Comment");

    //Comment fetch
    fetch('/ToDos/EditComment', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            CommentID: commentID,
            Comment: comment,
            Password: token,
            Email: myuser
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

            if (data.success) { //???
                //Refresh

                location.href = "/Todos/Comment?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
                    + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID;

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


    //Home
    //location.href = "/Todos/Comment?email=" + encodeURI(myuser) + "&username=" + encodeURI(myuser)
    //  + "&todo=" + encodeURI(todo) + "&title=" + encodeURI(title) + "&token=" + token + "&toDoID=" + toDoID + "&userID=" + userID;
}

function logout() {
    //alert("Loging todo out")
    location.href = "/Account/Logout";
}

function editProfileDetails(username, token) {

    //alert("ToDos");
    //alert(username);
    //alert(token);

    location.href = "/Account/Edit?userName=" + username + "&token=" + token
    alert(username);
}

function saveEditUserProfile(username, token) {
    //alert("ToDos");
    //alert(username);
    //alert(token);

    /*
    public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Identifier { get; set; }
        public string DateOfBirth { get; set; } 
        public string CellNumber { get; set; }
        */

    /*
 

 
 alert(username);*/
}