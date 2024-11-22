// Function to show a specific page and hide others
function showPage(pageId) {
    const pages = document.getElementsByClassName("page");
    for (let page of pages) {
        page.style.display = "none"; // Hide all pages
    }
    document.getElementById(pageId).style.display = "block"; // Show the selected page
}

// Event listener for the Register form submission
document.getElementById("registerForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const username = document.getElementById("registerUsername").value;
    const password = document.getElementById("registerPassword").value;

    try {
        const response = await fetch("http://localhost:5288/api/users/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ Username: username, PasswordHash: password })
        });

        if (response.ok) {
            alert("User registered successfully!");
            showPage('loginPage'); // Redirect to the login page after registration
        } else {
            const error = await response.text();
            alert("Error: " + error);
        }
    } catch (error) {
        console.error("Error during registration:", error);
    }
});

// Event listener for the Login form submission
document.getElementById("loginForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const username = document.getElementById("loginUsername").value;
    const password = document.getElementById("loginPassword").value;

    try {
        const response = await fetch("http://localhost:5288/api/users/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ Username: username, PasswordHash: password })
        });

        if (response.ok) {
            const data = await response.json(); // Wait for the response to be parsed as JSON
            console.log("Token received from server:", data); // Debug: Check if token is received correctly
            localStorage.setItem("token", data.token); // Store the JWT token
            localStorage.setItem("username", username); // Store the username
            alert("User logged in successfully!");
            showDashboard();
        } else {
            const error = await response.text();
            alert("Error: " + error);
        }

    } catch (error) {
        console.error("Error during login:", error);
    }
});

// Function to show the dashboard after login
function showDashboard() {
    const username = localStorage.getItem("username");
    document.getElementById("usernameDisplay").textContent = username;
    showPage('dashboardPage');
}

// Logout function
function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    alert("Logged out successfully!");
    showPage('mainPage');
}

// Check if the user is already logged in
document.addEventListener("DOMContentLoaded", () => {
    const token = localStorage.getItem("token");
    if (token) {
        showDashboard(); // Redirect to dashboard if token exists
    } else {
        showPage('mainPage'); // Show main page by default
    }
});

// Event listener for the Lobby creation form submission
document.getElementById("createLobbyForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const name = document.getElementById("lobbyName").value;
    const playerLimit = document.getElementById("playerLimit").value;
    const token = localStorage.getItem("token");

    try {
        const response = await fetch(`http://localhost:5288/api/lobbies/create?name=${name}&playerLimit=${playerLimit}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            const lobby = await response.json();
            console.log("Lobby created:", lobby);

            // Automatically join the lobby screen
            localStorage.setItem("lobbyId", lobby.id); // Store the lobby ID for future use
            showLobbyScreen(lobby.id); // Navigate to lobby screen
        } else {
            const error = await response.text();
            alert("Error: " + error);
        }
    } catch (error) {
        console.error("Error during lobby creation:", error);
    }
});

// Function to fetch and display available lobbies
async function fetchLobbies() {
    const token = localStorage.getItem("token");
    if (!token) {
        alert("You must be logged in to view lobbies.");
        return;
    }

    try {
        const response = await fetch("http://localhost:5288/api/lobbies", {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        console.log("Token being sent:", token);

        if (response.ok) {
            const lobbies = await response.json();
            const lobbiesList = document.getElementById("lobbiesList");
            lobbiesList.innerHTML = ''; // Clear current list

            lobbies.forEach(lobby => {
                const listItem = document.createElement("li");
                listItem.textContent = `Lobby: ${lobby.name}, Players: ${lobby.currentPlayers}/${lobby.playerLimit}`;

                // Create a "Join" button for each lobby
                const joinButton = document.createElement("button");
                joinButton.textContent = "Join";
                joinButton.onclick = () => joinLobby(lobby.id); // Call the joinLobby function when clicked
                listItem.appendChild(joinButton); // Append the join button to the list item

                lobbiesList.appendChild(listItem); // Append the list item to the list
            });

            showPage('lobbiesPage'); // Show the lobbies page
        } else {
            const error = await response.text();
            alert("Error fetching lobbies: " + error);
        }
    } catch (error) {
        console.error("Error fetching lobbies:", error);
    }
}

// Function to join a lobby
async function joinLobby(lobbyId) {
    const token = localStorage.getItem("token");

    if (!token) {
        alert("You must be logged in to join a lobby.");
        return;
    }

    try {
        const response = await fetch(`http://localhost:5288/api/lobbies/join/${lobbyId}`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            alert("Successfully joined the lobby!");

            // Store the lobby ID in local storage for future use
            localStorage.setItem("lobbyId", lobbyId);

            // Show the lobby screen and initialize the chat
            showLobbyScreen(lobbyId);
        } else {
            const error = await response.text();
            alert("Error joining lobby: " + error);
        }
    } catch (error) {
        console.error("Error joining lobby:", error);
    }
}

async function leaveLobby(lobbyId) {
    const token = localStorage.getItem("token");

    if (!token) {
        alert("You must be logged in to leave a lobby.");
        return;
    }

    try {
        const response = await fetch(`http://localhost:5288/api/lobbies/leave/${lobbyId}`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            alert("Successfully left the lobby!");

            // Close the SignalR connection when leaving the lobby
            await closeConnection();

            fetchLobbies(); // Refresh lobbies list
        } else {
            const error = await response.text();
            alert("Error leaving lobby: " + error);
        }
    } catch (error) {
        console.error("Error leaving lobby:", error);
    }
}


let connection = null; // Global variable to hold the SignalR connection

async function showLobbyScreen(lobbyId) {
    showPage('lobbyScreen');

    const token = localStorage.getItem("token");

    try {
        const response = await fetch(`http://localhost:5288/api/lobbies/${lobbyId}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            const lobby = await response.json();

            // Update the lobby details
            document.getElementById("lobbyNameDisplay").textContent = lobby.name;
            const playersList = document.getElementById("playersList");
            playersList.innerHTML = ''; // Clear the list

            // Display players and their ready status
            lobby.players.forEach(player => {
                const listItem = document.createElement("li");
                listItem.textContent = `${player.username} - ${player.isReady ? "Ready" : "Not Ready"}`;
                playersList.appendChild(listItem);
            });

            // Set up ready button
            const readyButton = document.getElementById("readyButton");
            const currentPlayer = lobby.players.find(p => p.username === localStorage.getItem("username"));
            readyButton.textContent = currentPlayer.isReady ? "Unready" : "Ready";
            readyButton.onclick = async () => {
                await toggleReadyStatus(lobbyId);
                await showLobbyScreen(lobbyId); // Refresh the lobby screen
            };

            // Set up leave button
            document.getElementById("leaveLobbyButton").onclick = () => leaveLobby(lobbyId);

            // Set up start game button
            document.getElementById("startGameButton").onclick = () => startGame(lobbyId);

        } else {
            const error = await response.text();
            alert("Error fetching lobby details: " + error);
        }
    } catch (error) {
        console.error("Error fetching lobby details:", error);
    }

    // Check if there's an existing connection
    if (connection && connection.state === signalR.HubConnectionState.Connected) {
        console.log("SignalR connection already established.");
        return;
    }

    // Create a new connection if none exists
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5288/chatHub", { accessTokenFactory: () => localStorage.getItem("token") })
        .withAutomaticReconnect() // Optionally, automatically reconnect if the connection is lost
        .build();

    try {
        // Start the connection
        await connection.start();
        console.log("Connected to the chat hub");

        // Join the lobby group
        await connection.invoke("JoinLobby", lobbyId);

        // Listen for incoming messages
        connection.on("ReceiveMessage", (user, message) => {
            const chatBox = document.getElementById("chatBox");
            const newMessage = document.createElement("div");
            newMessage.textContent = `${user}: ${message}`;
            chatBox.appendChild(newMessage);
        });

        // Handle sending messages
        document.getElementById("sendMessageButton").addEventListener("click", async function () {
            const message = document.getElementById("chatMessage").value;
            const username = localStorage.getItem("username");

            if (message) {
                await connection.invoke("SendMessageToLobby", lobbyId, username, message);
                document.getElementById("chatMessage").value = ""; // Clear the input field
            }
        });

    } catch (error) {
        console.error("Error connecting to chat hub:", error);
    }
}

// Function to close the existing SignalR connection (if needed)
async function closeConnection() {
    if (connection && connection.state === signalR.HubConnectionState.Connected) {
        await connection.stop();
        console.log("Connection closed.");
    }
}

// Function to toggle the ready status of the current player
async function toggleReadyStatus(lobbyId) {
    const token = localStorage.getItem("token");

    if (!token) {
        alert("You must be logged in to toggle ready status.");
        return;
    }

    try {
        const response = await fetch(`http://localhost:5288/api/lobbies/ready/${lobbyId}`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (!response.ok) {
            const error = await response.text();
            alert("Error toggling ready status: " + error);
        }
    } catch (error) {
        console.error("Error toggling ready status:", error);
    }
}

// Function to start a game in a lobby
async function startGame(lobbyId) {
    const token = localStorage.getItem("token");

    if (!token) {
        alert("You must be logged in to start a game.");
        return;
    }

    try {
        const response = await fetch(`http://localhost:5288/api/lobbies/start/${lobbyId}`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            alert("Game started successfully!");
            fetchLobbies(); // Refresh lobbies list
        } else {
            const error = await response.text();
            alert("Error starting game: " + error);
        }
    } catch (error) {
        console.error("Error starting game:", error);
    }
}
