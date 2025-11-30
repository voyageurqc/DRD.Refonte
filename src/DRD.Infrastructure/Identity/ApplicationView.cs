// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ApplicationView.cs
// Type de fichier                Infrastructure Entity
// Nature C#                      Class
// Emplacement                    Identity/
// Auteur                         Michel Gariépy
// Créé le                        2025-07-02
//
// Description
//     Représente une vue (Controller + Action) pouvant être liée à un type
//     d’accès utilisateur. Sert au système d’autorisation interne.
//
// Fonctionnalité
//     - Identifie une action de l’application via un code unique.
//     - Permet la localisation bilingue (DescriptionFr / DescriptionEn).
//     - Gère les relations entre les vues et les droits d’accès.
//     - Hérite de UserAudit pour assurer un suivi complet des modifications.
//
// Modifications
//     2025-11-30    Nettoyage complet ; déplacement from Domain → Infrastructure.
//     2025-07-14    Ajustements initiaux.
// ============================================================================

using DRD.Infrastructure.Common;

namespace DRD.Infrastructure.Identity
{
	/// <summary>
	/// Représente une action contrôlable au niveau des permissions
	/// (Controller + Action) dans l'application DRD.
	/// </summary>
	public class ApplicationView : UserAudit
	{
		#region Identification

		/// <summary>
		/// Code unique représentant cette vue/action.
		/// (Clé naturelle configurée via EF)
		/// </summary>
		public string ViewCode { get; private set; } = string.Empty;

		#endregion


		#region MVC Mapping

		/// <summary>
		/// Nom du contrôleur associé (ex.: Client, Institution).
		/// </summary>
		public string Controller { get; private set; } = string.Empty;

		/// <summary>
		/// Nom de l’action associée (ex.: Index, Edit, Create).
		/// </summary>
		public string Action { get; private set; } = string.Empty;

		#endregion


		#region Descriptions

		/// <summary>
		/// Description française affichée dans les outils admin.
		/// </summary>
		public string DescriptionFr { get; private set; } = string.Empty;

		/// <summary>
		/// Description anglaise affichée dans les outils admin.
		/// </summary>
		public string? DescriptionEn { get; private set; }

		#endregion


		#region Relations

		/// <summary>
		/// Liste des droits d’accès liés à cette vue.
		/// </summary>
		public ICollection<UserViewAccess> ViewAccesses { get; private set; }
			= new List<UserViewAccess>();

		#endregion
	}
}
