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
            showPage('mainPage'); // Redirect to the main page after registration
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
            alert("User logged in successfully!");
            showPage('mainPage'); // Redirect to the main page after login
        } else {
            const error = await response.text();
            alert("Error: " + error);
        }
    } catch (error) {
        console.error("Error during login:", error);
    }
});
