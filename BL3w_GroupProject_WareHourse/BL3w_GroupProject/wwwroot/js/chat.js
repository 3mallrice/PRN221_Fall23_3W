"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var messagesList = document.getElementById("messagesList");

    // Create a new list item element
    var li = document.createElement("li");

    // Assign a class based on the index of the list item
    var index = messagesList.children.length;
    if (index % 2 !== 0) {
        li.classList.add("text-success");
    } else {
        li.classList.add("text-info");
    }

    // Set the content of the list item
    li.textContent = `${user}: ${message}`;

    // Append the list item to the messagesList
    messagesList.appendChild(li);

    // Check if the messagesList is scrolled to the bottom
    var isScrolledToBottom = messagesList.scrollHeight - messagesList.clientHeight <= messagesList.scrollTop + 1;

    // If scrolled to the bottom, auto-scroll to show the latest message
    if (isScrolledToBottom) {
        messagesList.scrollTop = messagesList.scrollHeight;
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});