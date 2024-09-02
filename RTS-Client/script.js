const canvas = document.getElementById('gameCanvas');
const ctx = canvas.getContext('2d');

const GAME_STATE = {
    resources: { gold: 100, wood: 100 },
    units: [],
    buildings: [],
    camera: { x: 0, y: 0 }
};

// Initialize SignalR connection
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5288/gameHub") // Your server's URL
    .build();

async function startConnection() {
    try {
        await connection.start();
        console.log("Connected to the game server.");
    } catch (err) {
        console.error("Error connecting to server:", err);
        setTimeout(startConnection, 5000); // Retry on failure
    }
}

// Handle server events
connection.on("PlayerJoined", (playerName) => {
    console.log(`${playerName} joined the game.`);
});

connection.on("ReceivePlayerAction", (playerId, action) => {
    console.log(`Player ${playerId} performed action: ${action}`);
});

async function joinGame(playerName) {
    try {
        await connection.invoke("JoinGame", playerName);
    } catch (err) {
        console.error("Error joining game:", err);
    }
}

async function playerAction(playerId, action) {
    try {
        await connection.invoke("PlayerAction", playerId, action);
    } catch (err) {
        console.error("Error sending player action:", err);
    }
}

function initGame() {
    ctx.font = '16px Arial';
    ctx.fillStyle = 'white';

    startConnection();
    renderGame();
}

function renderGame() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.fillText(`Gold: ${GAME_STATE.resources.gold}`, 10, 20);
    ctx.fillText(`Wood: ${GAME_STATE.resources.wood}`, 10, 40);
    requestAnimationFrame(renderGame);
}

initGame();
