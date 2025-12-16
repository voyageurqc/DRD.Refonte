# 📜 Standards et Règles de Développement — DRD

**Version : 2025-12-03**

Ce document constitue la **référence officielle** des standards techniques, des conventions et des politiques de développement applicables à **toute la solution DRD**.

Il doit être suivi **strictement et uniformément**, dans **tous les projets**, **tous les fichiers**, et par **tous les collaborateurs**.

---

## 🧭 1. Priorités de Développement (Règle Mère)

Les règles suivantes sont classées **par ordre de priorité**.

👉 **Toute décision de développement doit respecter cet ordre.**

---

## 🟥 I. Priorité Critique — Architecture, Sécurité, Structure

### 1. Architecture Clean Architecture (OBLIGATOIRE)

Structure officielle DRD :

```
DRD.Domain          → Entités, Interfaces, Enums, Logique métier
DRD.Application     → Services applicatifs, DTOs, Interfaces
DRD.Infrastructure  → EF Core, Identity, Repositories, Logging, Services
DRD.Web             → MVC, Views, Controllers, ViewModels
DRD.Resources       → Tous les fichiers .resx (bilingue)
```

**Règles impératives** :

* ❌ Aucun accès `Web → Infrastructure`, `Domain → Web`, etc.
* ❌ Aucune dépendance circulaire
* ✅ `Domain` **ne dépend jamais** d’un autre projet
* ✅ `Web` **ne contient aucune logique métier**

---

### 2. Sécurité ASP.NET Identity (OBLIGATOIRE)

* `[Authorize]` appliqué **globalement** via `AuthorizeFilter` dans `Program.cs`
* Seules les actions annotées `[AllowAnonymous]` sont accessibles publiquement
* Sécurité basée sur :

  * Rôles
  * `AccessType`
  * `SecurityLevel`
* Déconnexion sécurisée et résistante aux attaques

#### 🔐 Règle DRD spécifique

> **Un login est requis à chaque lancement de l’application.**

* Les cookies d’authentification émis avant le dernier démarrage sont invalidés
* Règle implémentée **centralement dans `Program.cs`**

---

### 3. Localisation & Dates

* Culture par défaut : **fr-CA**
* Cultures supportées : **fr-CA**, **en-CA**
* **Toutes les dates** dans le code utilisent exclusivement :

```csharp
DateTime.UtcNow
```

---

## 🟧 II. Priorité Fonctionnelle — Collaboration & Workflow DRD

### 4. Collaboration : règle d’or
⚠️ **Avant une correction, toujours vérifier si d'autre programmes sont affectés**

⚠️ **Un seul fichier à la fois. Jamais plusieurs.**

**Processus obligatoire** :

1. Le fichier exact est fourni
2. Analyse
3. Proposition
4. Fichier complet + explications
5. Validation
6. Commit & Push
7. Étape suivante

---

### 5. Aucune modification non validée

Toute amélioration doit impérativement :

* être **expliquée**
* être **justifiée**
* être **approuvée**
* être inscrite dans la **TODO** si reportée

---

## 🟨 III. Priorité Standard — Documentation, UI, Conventions

### 6. En-tête DRD obligatoire dans tous les fichiers

Tous les fichiers suivants **doivent** contenir un en-tête DRD :
TODO – Harmonisation des vues Razor (DRD v10)

Constat :
Les vues Razor du projet présentent actuellement une incohérence
dans l’ordre des éléments en tête de fichier (en-tête DRD, @model, @using).

Décision :
Aucune modification immédiate afin d’éviter toute régression
sur des écrans fonctionnels.

Action future :
Définir une règle DRD unique pour l’ordre des directives Razor
(@model / @using) et de l’en-tête DRD, puis appliquer l’harmonisation
en une seule passe contrôlée.

* `.cs`
* `.cshtml`
* `.razor`

**Règles absolues** :

* En-tête **jamais supprimé**
* Champs alignés avec des **TABs** (jamais des espaces)
* Sections standardisées
* Historique **trié du plus récent au plus ancien**

#### Format officiel

```csharp
// ============================================================================
// Projet      DRD.[Nom]
// Fichier     [NomFichier.ext]
// Type        [Type .NET : Contrôleur MVC, ViewModel, Service, etc.]
// Classe      [Nom de la classe / View]
// Emplacement [Chemin relatif]
// Entité(s)   [Liste des entités concernées]
// Créé le     YYYY-MM-DD
//
// Description
//     Résumé concis
//
// Fonctionnalité
//     - Point
//     - Point
//
// Modifications
//     YYYY-MM-DD    Modification la plus récente
// ============================================================================
```

---

### 7. UI DRD — Normes officielles

* **Bootstrap 5** obligatoire
* **Font Awesome + Emojis**
* Boutons **3D DRD** normalisés
* Ordre standard des actions :

  1. Voir
  2. Modifier
  3. Supprimer / Désactiver
* Tooltips **obligatoires**
* Styles dans `site.css` (pas d’inline sauf exception minimale)
* **DataTables bilingues partout**

---

### 8. Conventions de nommage

* Tous les ViewModels se terminent par `VM`

  * `LoginVM`
  * `RegisterVM`
  * `ClientListVM`

**Règles spécifiques aux Views `.cshtml`** :

* `@model` **doit être la toute première ligne**
* En-tête DRD complet obligatoire
* Utilisation de régions avec `/// <summary>`

---

## 🏛️ 2. Structure Officielle de la Solution DRD

### DRD.Domain

* Entités métiers
* Interfaces
* Enums
* ❌ Aucune dépendance externe

### DRD.Application

* Services + Interfaces
* DTOs / ViewModels mappés
* Règles métier de haut niveau

### DRD.Infrastructure

* EF Core / DbContext
* Identity (`ApplicationUser`, `AccessType`)
* Repositories + UnitOfWork
* Services externes (mail, PDF, etc.)

### DRD.Web

* MVC
* Controllers
* Views `.cshtml`
* ViewModels (VM)
* Toastr / Validation / DataTables

### DRD.Resources

* Tous les fichiers `.resx` (FR-CA / EN-CA)
* ❌ Aucun texte visible dans le code

---

## 🌍 3. Règles Ressources (.resx)

1. **Aucun texte visible dans le code**
2. Toutes les clés doivent exister (validation à la compilation)
3. Aucune clé manquante tolérée à l’exécution
4. Retours à la ligne **réels uniquement**
5. ❌ Jamais `\\n` ou `<br/>`

---

## 🔧 4. Workflow de Développement DRD

* Un seul fichier à la fois
* Fichier complet obligatoire
* Modifications accompagnées d’explications
* Validation avant toute suite
* Commit & Push

---

## 📌 5. Ajouts du 2025-12-03

* Intégration du template `.cshtml` DRD
* Règle `@model` en première ligne
* Historique trié du plus récent au plus ancien
* Validation renforcée des `.resx`
* Révision `Program.cs` (Authorize global + Serilog)
* Normalisation complète du module Identity

---

## 📚 6. Historique du README.md

* **2025-12-03** — Mise à jour majeure (v10)
* 2025-11-29 — Intégration Serilog + règles metadata
* 2025-11-20 — Adoption Clean Architecture stricte

---

🎯 **Fin du README — DRD Standards Officiels**
