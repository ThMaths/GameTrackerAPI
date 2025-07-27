# 🎮 GameTracker API

Une API RESTful ASP.NET Core pour gérer votre collection de jeux vidéo, avec :

* **CRUD** basique (POST / GET / PUT / DELETE)
* Suivi du statut "terminé" (`IsCompleted`)
* **Authentification JWT** (login / token Bearer)
* Documentation **Swagger** interactive
* **POC frontend** statique (HTML / CSS / JS) dans `wwwroot`

---

## 📋 Fonctionnalités

* **Ajouter** / **lister** / **récupérer** / **mettre à jour** / **supprimer** des jeux 
* Champ booléen `IsCompleted` pour indiquer si un jeu est terminé 
* **Login** via un compte de test pour générer un JWT 
* Endpoints **protégés** (`POST`, `PUT`, `DELETE`) nécessitant un Bearer token 
* Documentation **Swagger UI** avec bouton **Authorize** pour injecter le token 
* **Front** statique (dans `wwwroot/index.html`) consommant l’API

---

## 🚀 Prérequis

* [.NET SDK 7 (ou supérieur)](https://dotnet.microsoft.com/download)
* Navigateur moderne (Chrome, Edge, Firefox…)
* (Optionnel) Postman ou curl pour tester l’API

---

## 🔧 Installation & démarrage

1. **Cloner le dépôt**

   ```bash
   git clone https://votre-repo/GameTrackerAPI.git
   cd GameTrackerAPI
   ```

2. **Configurer les paramètres**

   * Le fichier `appsettings.json` contient déjà la section "Jwt" avec la clé, l'émetteur et l'audience.
   * Vérifiez simplement que les valeurs correspondent à votre environnement.

3. **Lancer le backend**

   ```bash
   dotnet run
   ```

   L’API écoute par défaut sur :

   * `https://localhost:7218`
   * `http://localhost:5098`

4. **Accéder à Swagger UI**
   Ouvrez dans votre navigateur :

   ```
   https://localhost:7218/swagger
   ```

   * Effectuez un **POST** vers `/api/auth/login` avec vos identifiants de test.
   * Cliquez sur **Authorize**, collez `Bearer <votre_token>`.
   * Appelez ensuite les endpoints protégés.

5. **Utiliser le frontend statique**
   Ouvrez tout simplement :

   ```
   https://localhost:7270/
   ```

   Vous pourrez :

   * Vous connecter
   * Ajouter, éditer, supprimer et lister vos jeux

---

## 👤 Compte de test

| Rôle        | Email                 | Mot de passe |
| ----------- | --------------------- | ------------ |
| Utilisateur | `tester@gameapi.test` | `Test123!`   |

> Pour modifier ces identifiants, éditez les constantes dans `AuthController.cs`.

---

## 📁 Structure du projet

```
GameTrackerAPI/
│
├─ Controllers/
│   ├─ AuthController.cs
│   └─ GamesController.cs
│
├─ Models/
│   └─ Game.cs
│
├─ DTOs/
│   ├─ GameDto.cs
│   └─ LoginRequestDto.cs
│
├─ Services/
│   ├─ IGameService.cs
│   └─ GameService.cs
│
├─ wwwroot/
│   ├─ index.html
│   ├─ css/
│   │   └─ site.css
│   └─ js/
│       └─ site.js
│
├─ appsettings.json
├─ Program.cs
├─ GameTrackerAPI.csproj
└─ README.md
```

---

## 🛠️ Tech Stack

* **.NET 7 / ASP.NET Core**
* **JWT Bearer** pour l’authentification
* **Swashbuckle** (Swagger/OpenAPI)
* **Vanilla JS + Bootstrap 5** pour le POC frontend
* **In-Memory** (Service singleton) – remplaçable par EF Core + base SQL

---

* **Auteur** : Thomas Mathias
