const API_URL = window.location.origin + "/api";

let token = "";
let editId = null;

document.addEventListener("DOMContentLoaded", () => {
    // Sélecteurs
    const loginSection = document.getElementById("loginSection");
    const appSection = document.getElementById("appSection");
    const loginBtn = document.getElementById("loginBtn");
    const logoutBtn = document.getElementById("logoutBtn");
    const loginMessage = document.getElementById("loginMessage");

    const saveBtn = document.getElementById("saveBtn");
    const cancelEditBtn = document.getElementById("cancelEditBtn");
    const formTitle = document.getElementById("formTitle");

    // Inputs
    const emailInput = document.getElementById("emailInput");
    const passwordInput = document.getElementById("passwordInput");
    const titleInput = document.getElementById("titleInput");
    const platformInput = document.getElementById("platformInput");
    const genreInput = document.getElementById("genreInput");
    const releaseDateInput = document.getElementById("releaseDateInput");
    const completedInput = document.getElementById("completedInput");

    // Événements
    loginBtn.addEventListener("click", login);
    logoutBtn.addEventListener("click", () => {
        token = "";
        appSection.classList.add("d-none");
        loginSection.classList.remove("d-none");
    });

    saveBtn.addEventListener("click", () => {
        if (editId === null) createGame();
        else updateGame(editId);
    });
    cancelEditBtn.addEventListener("click", resetForm);

    // Méthodes

    function login() {
        fetch(`${API_URL}/auth/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                email: emailInput.value,
                password: passwordInput.value
            })
        })
            .then(r => r.ok ? r.json() : Promise.reject("Invalid credentials"))
            .then(data => {
                token = data.token;
                loginSection.classList.add("d-none");
                appSection.classList.remove("d-none");
                loadGames();
            })
            .catch(err => {
                loginMessage.textContent = "Échec de la connexion.";
                console.error(err);
            });
    }

    function loadGames() {
        fetch(`${API_URL}/games`, {
            headers: { "Authorization": `Bearer ${token}` }
        })
            .then(r => r.json())
            .then(games => {
                const tbody = document.getElementById("gamesTbody");
                tbody.innerHTML = "";
                games.forEach(g => {
                    const tr = document.createElement("tr");
                    tr.innerHTML = `
          <td>${g.id}</td>
          <td>${g.title}</td>
          <td>${g.platform}</td>
          <td>${g.isCompleted ? "✅" : "❌"}</td>
          <td>
            <button class="btn btn-sm btn-primary me-1" onclick="editGame(${g.id})">✏️</button>
            <button class="btn btn-sm btn-danger" onclick="deleteGame(${g.id})">🗑️</button>
          </td>`;
                    tbody.appendChild(tr);
                });
            })
            .catch(console.error);
    }

    window.editGame = function (id) {
        fetch(`${API_URL}/games/${id}`, {
            headers: { "Authorization": `Bearer ${token}` }
        })
            .then(r => r.ok ? r.json() : Promise.reject())
            .then(g => {
                editId = id;
                formTitle.textContent = "Modifier un jeu";
                cancelEditBtn.classList.remove("d-none");
                titleInput.value = g.title;
                platformInput.value = g.platform;
                genreInput.value = g.genre;
                releaseDateInput.value = g.releaseDate.split("T")[0];
                completedInput.checked = g.isCompleted;
                saveBtn.textContent = "Mettre à jour";
            })
            .catch(console.error);
    };

    function createGame() {
        const dto = {
            title: titleInput.value,
            platform: platformInput.value,
            genre: genreInput.value,
            releaseDate: releaseDateInput.value,
            isCompleted: completedInput.checked
        };
        fetch(`${API_URL}/games`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(dto)
        })
            .then(r => { if (!r.ok) return r.json().then(e => Promise.reject(e)); return r.json(); })
            .then(() => {
                resetForm();
                loadGames();
            })
            .catch(err => alert("Erreur création : " + JSON.stringify(err)));
    }

    function updateGame(id) {
        const dto = {
            title: titleInput.value,
            platform: platformInput.value,
            genre: genreInput.value,
            releaseDate: releaseDateInput.value,
            isCompleted: completedInput.checked
        };
        fetch(`${API_URL}/games/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(dto)
        })
            .then(r => {
                if (r.ok) {
                    resetForm();
                    loadGames();
                } else {
                    return r.json().then(e => Promise.reject(e));
                }
            })
            .catch(err => alert("Erreur mise à jour : " + JSON.stringify(err)));
    }

    window.deleteGame = function (id) {
        if (!confirm("Supprimer ce jeu ?")) return;
        fetch(`${API_URL}/games/${id}`, {
            method: "DELETE",
            headers: { "Authorization": `Bearer ${token}` }
        })
            .then(r => {
                if (r.ok) loadGames();
                else alert("Erreur suppression");
            })
            .catch(console.error);
    };

    function resetForm() {
        editId = null;
        formTitle.textContent = "Ajouter un jeu";
        cancelEditBtn.classList.add("d-none");
        saveBtn.textContent = "Enregistrer";
        titleInput.value = "";
        platformInput.value = "";
        genreInput.value = "";
        releaseDateInput.value = "";
        completedInput.checked = false;
    }

});
