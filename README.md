📜 Standards et Règles de Développement — DRD

Version : 2025-12-03

Ce document constitue la référence officielle des standards techniques, des conventions et des politiques de développement applicables à toute la solution DRD.
Il doit être suivi strictement et uniformément, dans tous les projets, tous les fichiers, et par tous les collaborateurs.

🧭 1. Priorités de Développement (Règle Mère)

Les règles suivantes sont classées par ordre de priorité.
Chaque décision de développement doit respecter cet ordre.

🟥 I. Priorité Critique — Architecture, Sécurité, Structure
1. Architecture Clean Architecture (obligatoire)
DRD.Domain        → Entités, Interfaces, Enums, Logique métier
DRD.Application  → Services applicatifs, DTOs, Interfaces
DRD.Infrastructure → EF Core, Identity, Repositories, Logging, Services
DRD.Web          → MVC, Views, Controllers, ViewModels
DRD.Resources    → Tous les fichiers .resx (bilingue)


Règles :

Aucun accès Web → Infrastructure, Domain → Web, etc.

Domain ne dépend jamais d’autre chose.

Web ne contient aucune logique métier.

2. Sécurité ASP.NET Identity (obligatoire)

[Authorize] appliqué globalement via AuthorizeFilter dans Program.cs.

Seules les actions annotées [AllowAnonymous] sont accessibles publiquement.

Gestion des rôles + AccessType + SecurityLevel conforme DRD v10.

Déconnexion sécurisée et résistante aux attaques.

3. Localisation & Dates

Culture par défaut : fr-CA

Cultures supportées : fr-CA, en-CA

Toutes les dates assignées dans le code utilisent :

DateTime.UtcNow

🟧 II. Priorité Fonctionnelle — Collaboration & Workflow DRD
4. Collaboration : règle d’or

Un seul fichier à la fois. Jamais plusieurs.

Processus obligatoire :

Tu fournis le fichier exact.

Analyse.

Proposition.

Fichier complet + explications.

Validation.

Commit & Push.

Étape suivante.

5. Pas de modifications non validées

Toute amélioration doit :

être expliquée

être justifiée

être approuvée

être inscrite dans la TODO si reportée

🟨 III. Priorité Standard — Documentation, UI, Conventions
6. En-tête DRD obligatoire dans tous les fichiers
// ============================================================================
// Projet      DRD.[Nom]
// Fichier     [NomFichier.ext]
// Type        [Type .NET : Contrôleur MVC, ViewModel, Service, etc.]
// Classe      [Nom de la classe / View]
// Emplacement [Chemin relatif]
// Entité(s)   [Liste des entités concernées]
// Créé le     YYYY-MM-DD
//
// Description:
//     [Résumé concis]
//
// Fonctionnalité:
//     - [Point]
//     - [Point]
//
// Modifications:
//     YYYY-MM-DD: [Modif la plus récente]
//     YYYY-MM-DD: [Modif précédente]
// ============================================================================


Fichiers .cs, .cshtml, .razor

Champs alignés avec TABs

Sections standardisées

Section Modifications triée du plus récent au plus ancien

ℹ️ Le modèle complet se trouve à la section 4.

7. UI DRD — Normes officielles

Bootstrap 5

Font Awesome + Emojis

Boutons 3D DRD

Ordre standard :
Voir → Modifier → Supprimer

Tooltips obligatoires

Styles dans site.css, jamais inline sauf exceptions minimes

DataTables bilingue partout

8. Conventions de nommage

Tous les ViewModels finissent par VM

LoginVM

RegisterVM

ClientListVM

Les Views .cshtml doivent :

avoir @model en toute première ligne

contenir un en-tête DRD complet

utiliser des régions avec /// <summary>

🏛️ 2. Structure Officielle de la Solution DRD
DRD.Domain

Entités métiers

Interfaces

Enums

Pas de dépendances extérieures

DRD.Application

Services + Interfaces

DTOs / VM mappés

Règle métier de haut niveau

DRD.Infrastructure

EF Core / DbContext

Identity (ApplicationUser, AccessType)

Logique externe (mail, PDF)

Repositories + UnitOfWork

DRD.Web

MVC

Controllers

Views .cshtml

ViewModels (VM)

Toastr / Validation / DataTables

DRD.Resources

Tous les fichiers .resx (FR-CA et EN-CA)

Aucun texte visible ne doit rester dans le code

🖼️ 3. Documentation : En-tête Officiel DRD
3.1 Format obligatoire
// ============================================================================
// Projet:      DRD.[Nom]
// Fichier:     [NomFichier.ext]
// Type:        [Type .NET : Contrôleur MVC, ViewModel, Service, etc.]
// Classe:      [Nom de la classe / View]
// Emplacement: [Chemin relatif]
// Entité(s):   [Liste des entités concernées]
// Créé le:     YYYY-MM-DD
//
// Description:
//     [Résumé concis]
//
// Fonctionnalité:
//     - [Point]
//     - [Point]
//
// Modifications:
//     YYYY-MM-DD: [Modif la plus récente]
//     YYYY-MM-DD: [Modif précédente]
// ============================================================================

📝 4. Template Officiel — Views .cshtml

Intégré officiellement aux règles DRD

@model [Namespace].[NomDuVM]

@* ============================================================================
Projet                         DRD.Web
Nom du fichier                 [Nom].cshtml
Type de fichier                Razor View (MVC)
Classe                         [Nom de la Page]
Emplacement                    Views/[Dossier]
Entités concernées             [Liste]
Créé le                        YYYY-MM-DD

Description
    [Description claire de la vue.]

Fonctionnalité
    - [Point]
    - [Point]

Modifications
    YYYY-MM-DD    [Modif la plus récente]
    YYYY-MM-DD    [Modif précédente]
============================================================================ *@

@{
    Layout = "~/Views/Shared/_Layout.cshtml";
    ViewData["Title"] = "[Titre]";
}

@* ============================================================================
   Région : Styles
   ----------------------------------------------------------------------------
   /// <summary>
   /// Styles spécifiques à cette page.
   /// </summary>
   ========================================================================== *@

@* ============================================================================
   Région : Conteneur principal
   ----------------------------------------------------------------------------
   /// <summary>
   /// Structure principale de la vue.
   /// </summary>
   ========================================================================== *@

@* ============================================================================
   Région : Scripts
   ----------------------------------------------------------------------------
   /// <summary>
   /// Scripts liés à cette page (validation, JS DRD, etc.).
   /// </summary>
   ========================================================================== *@
@section Scripts {
}

🌍 5. Règles Ressources (.resx)
1. Aucun texte visible dans le code

Tous les messages doivent provenir des fichiers ressources :

Common.resx

Toastr.resx

FieldNames.resx

Popups.resx

2. Les clés doivent exister → validation à la compilation

Aucune exécution ne doit échouer pour une clé .resx manquante.

Les erreurs doivent être détectées avant runtime.

🔧 6. Workflow de Développement DRD

Un seul fichier à la fois

Le fichier complet est toujours requis

Modification fournie avec explications

Validation

Commit & Push

Étape suivante

📌 7. Ajouts du 2025-12-03

Intégration du template .cshtml DRD

Ajout de la règle @model en première ligne

Ajout de la règle historique trié du plus récent au plus vieux

Validation des .resx renforcée

Révision Program.cs (Authorize global + Serilog)

Clarification du rôle des projets DRD

Normalisation complète du module Identity

📚 8. Historique du README.md

2025-12-03 : Mise à jour majeure (v10)

2025-11-29 : Intégration Serilog + règles metadata

2025-11-20 : Adoption Clean Architecture stricte

🎯 Fin du README