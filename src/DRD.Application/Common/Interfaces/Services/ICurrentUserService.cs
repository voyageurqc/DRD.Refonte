// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 ICurrentUserService.cs
// Type de fichier                Interface
// Classe                         ICurrentUserService
// Emplacement                    Common/Interfaces
// Entités concernées             (Aucune — service transversal)
// Créé le                        2025-12-07
//
// Description
//     Fournit les informations relatives à l'utilisateur courant tel que
//     capturé via l'infrastructure Web (HttpContext). Utilisé pour l’audit
//     des entités et pour les opérations nécessitant une identification.
//
// Fonctionnalité
//     - Obtenir l'identifiant de l'utilisateur connecté.
//     - Obtenir le nom d'utilisateur.
//     - Déterminer si l'utilisateur est authentifié.
//
// Modifications
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

namespace DRD.Application.Common.Interfaces
{
	/// <summary>
	/// Définit les informations disponibles à propos de l'utilisateur courant.
	/// </summary>
	public interface ICurrentUserService
	{
		/// <summary>
		/// Identifiant unique de l'utilisateur (ou "Anonymous").
		/// </summary>
		string UserId { get; }

		/// <summary>
		/// Nom d'utilisateur (ou "Anonymous").
		/// </summary>
		string UserName { get; }

		/// <summary>
		/// Indique si l'utilisateur est authentifié.
		/// </summary>
		bool IsAuthenticated { get; }
	}
}
