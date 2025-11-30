//2025-11-30- 16:35 
📜 Standards et Règles de Codification
Ce document sert de guide de référence unique pour toutes les conventions, règles et standards de développement à suivre pour le projet.

## 🧭 1. Priorités de Développement (Règles Fondamentales)

Ces règles dictent l'ordre d'importance et la manière dont toutes les autres règles doivent être appliquées.

toujours utiliser les fonctionnalités de base pour correction, on corrige les fichiers courants non les fichiers de base.
Liste des fichiers de base

Fichiers avec Generique dans le nom
Fichiers avec Base dans le nom

### I. Priorité Critique (Architecture & Sécurité)

Ces règles définissent la structure et la sécurité de l'application et sont **non négociables**.

* **Architecture (Règle 2)**: Utiliser une architecture **N-Tier/Clean Architecture** avec séparation des responsabilités. Le `DRD.Domain` (cœur) ne doit dépendre d'aucune autre couche.
* **Sécurité (Règle 9)**: Utiliser **ASP.NET Identity** et appliquer `[Authorize]` globalement. L'accès doit être contrôlé par **Rôle + SecurityLevel**.
* **Dates & Localisation (Règle 7)**:
    * La culture par défaut de l'application est **fr-CA**.
    * Toutes les dates assignées dans le code doivent utiliser le **temps universel coordonné (DateTime.UtcNow)**.

### II. Priorité Fonctionnelle (Modèle & Flux de Travail)

Ces règles dictent la manière dont le code est écrit et la façon dont nous collaborons.

* **Contrat de Base (Priorité Absolue)**: L'architecture actuelle de votre projet a **préséance** sur ce `README.md`.
* **Dépendance Code/Modèle**: Nous ne modifions pas les fichiers d'héritage (Generic, Base, etc.), mais ajustons les modules spécifiques pour qu'ils soient **conformes** à ces bases.
* **Flux de Travail (Règle 4)**:
    * Procéder **un fichier à la fois**.
    * Je dois vous demander votre **code actuel** avant toute modification.
    * Utiliser **`FinancialInstitution-branches`** comme modèle de référence pour les nouveaux modules.
* **Localisation (Règle 7)**: Toutes les chaînes de caractères visibles par l'utilisateur doivent provenir des fichiers **`.resx`** du projet `DRD.Resources`.

### III. Priorité Standard (Documentation & Nommage)

Ces règles assurent la lisibilité, la maintenabilité et la cohérence visuelle.

* **Documentation (Règle 3)**: L'**en-tête de fichier** standardisé est **fondamental et obligatoire** sur chaque fichier, avec les deux-points (`:`) alignés et l'historique des modifications mis à jour.
* **Interface Utilisateur (Règle 5)**:
    * Utiliser **DataTables.net** (support bilingue) pour les tables de données.
    * Utiliser **Bootstrap 5**.
    * Standardisation des boutons (ex: `btn-3d`, ordre **Visualiser, Modifier, Supprimer**).
    * Icônes : **Font Awesome** (les emojis sont encouragés).
    * Tous les messages **Toastr** et **Popups** doivent provenir des fichiers `DRD.Resources`.
* **Conventions de Code (Règle 6)**: Les ViewModels doivent se terminer par le suffixe **VM** (ex: `InstitutionListVM`).

---

## 🏛️ 2. Architecture du Projet

Le projet est structuré en plusieurs couches distinctes, suivant les principes de la Clean Architecture pour garantir une séparation claire des responsabilités.

* **DRD.Domain**: 💜 Le cœur de l'application. Il contient les entités, les interfaces des services et des repositories. Il ne dépend d'aucune autre couche.
* **DRD.Infrastructure**: ⚙️ La couche technique. Elle contient les implémentations concrètes : DbContext, Repositories, migrations, etc.
* **DRD.Application**: 🧠 La couche d'orchestration. Elle contient la logique applicative (Services) et les objets de transfert de données (DTOs).
* **DRD.Web**: 🖥️ La couche de présentation (ASP.NET Core MVC). Elle gère les Controllers, les ViewModels et les Views.
* **DRD.Resources**: 🌍 La couche de localisation. Elle centralise tous les fichiers de ressources (.resx).

## ✍️ 3. Documentation des Fichiers
dans mon 
La documentation est une règle fondamentale et doit être suivie sans exception.

### 3.1. En-tête de Fichier

Chaque fichier doit commencer par le bloc d'en-tête suivant, avec les deux-points (:) parfaitement alignés. L'historique des modifications doit être préservé et mis à jour à chaque intervention.

```csharp
// ============================================================================
// Projet:      [Nom du projet, ex: DRD.Web]
// Fichier:     [Nom complet du fichier, ex: ClientService.cs]
// Type:        [Rôle fonctionnel, ex: Service (Svc)]
// Classe:      [Type de construction, ex: Class]
// Emplacement: [Chemin relatif depuis la racine du projet, ex: Services/]
// Entité(s):   [Entité(s) principale(s) concernée(s), ex: Client]
// Créé le:     [Date de création, ex: YYYY-MM-DD]
//
// Description:
//     [Description brève et claire du but du fichier.]
//
// Fonctionnalité:
//     - [Liste à puces des responsabilités ou des actions clés du fichier.]
//
// Modifications:
//     [YYYY-MM-DD]: [Description de la modification.]
// ============================================================================
