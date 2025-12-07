// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 MetadataMapper.cs
// Type de fichier                Classe utilitaire
// Classe                         MetadataMapper
// Emplacement                    Controllers/Helpers
// Entités concernées             ViewModels auditables (ex.: CdSetEditVM)
// Créé le                        2025-12-08
//
// Description
//     Fournit des méthodes d’extension permettant de convertir des ViewModels
//     contenant les champs d’audit (CreationDate, CreatedBy, etc.) en un
//     EntityMetadataDto destiné à l'affichage dans les vues (partials, modales).
//
// Fonctionnalité
//     - Transformer un ViewModel en DTO utilisable par les partials Metadata.
//     - Supporter l’ensemble du système de métadonnées DRD (vues Edit/Details).
//     - Ne dépendre d’aucune entité Domain (respect MVC).
//
// Modifications
//     2025-12-08    Version DRD v10 – suppression SecurityLevel, ajout régions.
// ============================================================================

using System;
using DRD.Application.Popup.Models;

namespace DRD.Web.Controllers.Helpers
{
	/// <summary>
	/// Méthodes d’extension permettant de construire un DTO de métadonnées
	/// à partir d’un ViewModel contenant les champs d’audit nécessaires.
	/// </summary>
	public static class MetadataMapper
	{
		// ============================================================================
		#region Conversion depuis un ViewModel
		/// <summary>
		/// Convertit un ViewModel contenant les champs d’audit standard en un
		/// <see cref="EntityMetadataDto"/> utilisable par les modales et partials DRD.
		/// </summary>
		/// <typeparam name="T">
		/// Type du ViewModel. Il doit exposer les propriétés d’audit suivantes :
		/// CreationDate, CreatedBy, ModificationDate, UpdatedBy.
		/// </typeparam>
		/// <param name="vm">ViewModel source.</param>
		/// <returns>DTO contenant les informations d’audit.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static EntityMetadataDto ToMetadataDto<T>(this T vm)
			where T : class
		{
			if (vm == null)
				throw new ArgumentNullException(nameof(vm));

			// Reflection minimale (mais contrôlée) : DRD ViewModels sont homogènes
			var type = vm.GetType();

			return new EntityMetadataDto
			{
				CreationDate = type.GetProperty("CreationDate")?.GetValue(vm) as DateTime? ?? DateTime.MinValue,
				CreatedBy = type.GetProperty("CreatedBy")?.GetValue(vm) as string?,
				ModificationDate = type.GetProperty("ModificationDate")?.GetValue(vm) as DateTime? ?? DateTime.MinValue,
				UpdatedBy = type.GetProperty("UpdatedBy")?.GetValue(vm) as string?
			};
		}
		#endregion
		// ============================================================================
	}
}
