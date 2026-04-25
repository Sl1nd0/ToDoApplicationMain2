//alert("Inside Log in");

var fullPath = window.location.pathname;
var directoryPath = fullPath.substring(0, fullPath.lastIndexOf('/'));

//alert("Current directory path:", fullPath);

function utf8ToBase64(str) {
    const encoder = new Textencoder();
    const data = encoder.encode(str);

    const binaryString = String.fromCharCode.apply(nul, data);

    return btoa(binaryString);
}

function base64ToUtf8(encodedStr) {
    const binaryString = atob(encodedStr);
    const bytes = new Uint8Array(binaryString.length);

    for (let i = 0; i < binaryString.length; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }
    const decoder = new TextDecoder('utf-8');

    return decoder.decode(bytes);
}

function b64Encode(str) {
    const encoder = new TextEncoder();
    const uint8Array = encoder.encode(str);

    //Convert the 
    let binaryString = uint8Array.forEach(byte => { binaryString += String.fromCharCode(byte)});

    return btoa(binaryString);
}

function b64Decode(str) {
    const binaryString = atob(encodedString);

    const bytes = new Uint8Array(binaryString.length)

    for (let i = 0; i < binaryString.length; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }

    const decoder = new TextDecoder('utf-8');

    return decoder.decode(bytes);

}

document.getElementById("login").onclick = function () {

    //alert("login");

    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;

    if (email === ""
        || password === "") {
        alert("Please fill in all fields.");
        return;
    }

    fetch('/Account/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Email: email,
            //Username: username,
            Password: password
        })
    })
        .then(response => {

            console.log(JSON.stringify(response));

            if (response.ok) {
                return response.json();
            } else {
                response.text().then(text => alert("Error: " + text));
            }

        }).then(data => {
            //console.log('Parsed JSON:', data.success); // This contains the actual data
            //alert("reaches");

            //console.log(data)

            if (data.email) {
                //alert("Account created successfully!");
                //location.href = "/Account/index";

                // fetch('/Account/Index', {
                //     method: 'POST',
                //     headers: {
                //         'Content-Type': 'application/json'
                //     },
                //     body: JSON.stringify({
                //         Email: email,
                //         Username: email,
                //         Password: password
                //     })
                // }).then(response => {

                //     if (response.ok) {

                //        location.href = "/Account/Index?email="+ email + "&username="+email;

                //     } else {
                //         response.text().then(text => alert("Error: " + text));
                //     }
                // });

                //Feed the user to the index page with email and username

                //alert("data.userID");
                //alert(data.userID);

                //alert("data.userID");
                //var te = btoa(password);
                //alert(te);
                //var back = atob(te);
                //alert(back)
                //alert("data.userID");
                 
                //alert(testing);

                location.href = "/Account/Index?email=" + email + "&username=" + email + "&token=" + btoa(password) + "&userID=" + data.userID;
                //Post user and retain user
            } else {
                if (data.error) {
                    alert("Error: " + data.error);
                }
            }

        })
        .catch(error => {
            console.error('Logining');
            console.error(error);
            alert("An error occurred while loging into the account.");
        });
};  