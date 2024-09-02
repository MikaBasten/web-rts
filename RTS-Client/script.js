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
