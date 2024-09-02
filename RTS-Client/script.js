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
            const data = await response.json();
            localStorage.setItem("token", data.Token); // Store the JWT token
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

// Event listener for the Create Lobby form submission
document.getElementById("createLobbyForm").addEventListener("submit", async function (e) {
    e.preventDefault();
    
    const lobbyName = document.getElementById("lobbyName").value;
    const playerLimit = document.getElementById("playerLimit").value;
    const token = localStorage.getItem("token");

    if (!token) {
        alert("You must be logged in to create a lobby.");
        return;
    }

    try {
        const response = await fetch(`http://localhost:5288/api/lobbies?hostUserId=${encodeURIComponent(localStorage.getItem("username"))}&lobbyName=${encodeURIComponent(lobbyName)}&playerLimit=${playerLimit}`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            alert("Lobby created successfully!");
            showPage('dashboardPage');
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

        if (response.ok) {
            const lobbies = await response.json();
            const lobbiesList = document.getElementById("lobbiesList");
            lobbiesList.innerHTML = ''; // Clear current list

            lobbies.forEach(lobby => {
                const listItem = document.createElement("li");
                listItem.textContent = `Lobby: ${lobby.name}, Players: ${lobby.currentPlayers}/${lobby.playerLimit}`;
                listItem.onclick = () => joinLobby(lobby.id); // Click to join lobby
                lobbiesList.appendChild(listItem);
            });

            showPage('lobbiesPage');
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
    const username = localStorage.getItem("username");

    if (!token || !username) {
        alert("You must be logged in to join a lobby.");
        return;
    }

    try {
        const response = await fetch(`http://localhost:5288/api/lobbies/${lobbyId}/join?userId=${encodeURIComponent(username)}&username=${encodeURIComponent(username)}`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            alert("Successfully joined the lobby!");
            fetchLobbies(); // Refresh lobbies list
        } else {
            const error = await response.text();
            alert("Error joining lobby: " + error);
        }
    } catch (error) {
        console.error("Error joining lobby:", error);
    }
}
