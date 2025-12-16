// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 IMetadataDisplayService.cs
// Type                           Interface
// Classe                         IMetadataDisplayService
// Emplacement                    Popup/Services/Metadata
// Entité(s) concernées           EntityMetadataDto, IAuditableEntity
// Créé le                        2025-12-16
//
// Description
//     Contrat applicatif responsable de la préparation des métadonnées
//     système destinées à l’affichage (popup Métadonnées).
//
// Fonctionnalité
//     - Construit un EntityMetadataDto prêt à afficher.
//     - Résout l’affichage utilisateur selon la règle DRD :
//           « Prénom Nom (email) ».
//     - Ne contient aucune logique UI ni accès direct aux vues.
//
// Modifications
//     2025-12-16    Création du contrat (AXE 1 – Métadonnées).
// ============================================================================

using DRD.Application.Popup.Models;
using DRD.Domain.Common;
using System.Threading.Tasks;

namespace DRD.Application.Popup.Services.Metadata
{
	/// <summary>
	/// Service applicatif chargé de construire les métadonnées système
	/// prêtes à afficher pour une entité auditable.
	/// </summary>
	public interface IMetadataDisplayService
	{
		/// <summary>
		/// Construit un DTO de métadonnées prêt à afficher pour une entité donnée.
		/// </summary>
		/// <param name="entity">Entité auditable source.</param>
		/// <returns>DTO de métadonnées prêt à afficher.</returns>
		Task<EntityMetadataDto> BuildAsync(IAuditableEntity entity);
	}
}
