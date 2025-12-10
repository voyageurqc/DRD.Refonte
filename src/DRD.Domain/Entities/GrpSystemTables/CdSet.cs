// ============================================================================
// Projet:      DRD.Domain
// Fichier:     CdSet.cs
// Type:        Entity (Domain)
// Classe:      CdSet
// Emplacement: Entities/GrpSystemTables
// Entité(s):   CdSet
// Créé le:     2025-06-17
//
// Description:
//     Entité représentant une entrée paramétrique générique (Code Set).
//     Chaque enregistrement appartient à une famille (TypeCode) et possède un
//     code unique dans cette famille, accompagné de descriptions bilingues.
//     Utilisé pour toutes les listes déroulantes et sélecteurs du projet.
//
// Fonctionnalité:
//     - Stockage centralisé de valeurs paramétriques (TypeCode + Code).
//     - Support natif bilingue (FR/EN).
//     - Validation et normalisation Domain strictes.
//     - Encapsulation via mutateurs Domain.
//     - Héritage de BaseAuditableEntity (audit complet + IsActive).
//
// Modifications:
//     2025-12-11	Ajout validation Domain (null/empty), Trim(), UpperCase TypeCode,
//					longueurs maximales, normalisation complète, commentaires XML.
//     2025-12-09	Ajustements DRD (en-tête, régions, résumés).
//     2025-12-07	Ajout des mutateurs Domain (SetFamily, SetCodeValue,
//					SetDescriptions, ModifyFields).
//     2025-11-30	Nettoyage Domain + suppression EF details.
//     2025-07-14	Refonte initiale (ancien projet).
//     2025-06-17	Création initiale.
// ============================================================================

using System;
using DRD.Domain.Common;

namespace DRD.Domain.Entities.GrpSystemTables
{
	/// <summary>
	/// Représente une entrée générique paramétrique au sein du projet DRD.
	/// La clé composite logique est (TypeCode, Code), avec descriptions FR/EN.
	/// Toutes les validations et normalisations Domain sont appliquées via les mutateurs.
	/// </summary>
	public class CdSet : BaseAuditableEntity
	{
		// --------------------------------------------------------------------
		// REGION : Identifiants
		// --------------------------------------------------------------------
		#region Identification
		/// <summary>
		/// Nom de la famille (TypeCode) regroupant plusieurs valeurs (Code).
		/// Stocké en UPPERCASE + Trim pour maintenir une clé cohérente.
		/// </summary>
		public string TypeCode { get; private set; } = string.Empty;

		/// <summary>
		/// Code unique appartenant à la famille (TypeCode).
		/// Trim() appliqué systématiquement.
		/// </summary>
		public string Code { get; private set; } = string.Empty;
		#endregion

		// --------------------------------------------------------------------
		// REGION : Descriptions
		// --------------------------------------------------------------------
		#region Descriptions
		/// <summary>Description française du code (Trim appliqué).</summary>
		public string DescriptionFr { get; private set; } = string.Empty;

		/// <summary>Description anglaise du code (Trim appliqué).</summary>
		public string? DescriptionEn { get; private set; }
		#endregion


		// --------------------------------------------------------------------
		// REGION : Mutateurs (Logique Domain)
		// --------------------------------------------------------------------
		#region Mutateurs

		/// <summary>
		/// Définit la famille (TypeCode) avec validation + normalisation.
		/// </summary>
		/// <param name="typeCode">Nom de la famille à assigner.</param>
		/// <exception cref="ArgumentException">Si valeur vide ou trop longue.</exception>
		public void SetFamily(string typeCode)
		{
			// TODO: Ajouter bilinguisme pour les messages d'erreur si nécessaire.
			if (string.IsNullOrWhiteSpace(typeCode))
				throw new ArgumentException("TypeCode cannot be empty.");

			typeCode = typeCode.Trim();

			if (typeCode.Length > 20)
				throw new ArgumentException("TypeCode exceeds maximum length of 20 characters.");

			TypeCode = typeCode.ToUpperInvariant();
		}

		/// <summary>
		/// Définit la valeur du code dans la famille.
		/// Trim + validation Domain + longueur maximale.
		/// </summary>
		public void SetCodeValue(string code)
		{
			if (string.IsNullOrWhiteSpace(code))
				throw new ArgumentException("Code cannot be empty.");

			code = code.Trim();

			if (code.Length > 20)
				throw new ArgumentException("Code exceeds maximum length of 20 characters.");

			Code = code;
		}

		/// <summary>
		/// Définit les descriptions FR/EN (trim obligatoire, null permis pour EN).
		/// </summary>
		public void SetDescriptions(string fr, string? en)
		{
			if (string.IsNullOrWhiteSpace(fr))
				throw new ArgumentException("DescriptionFr cannot be empty.");

			fr = fr.Trim();
			en = en?.Trim();

			if (fr.Length > 50)
				throw new ArgumentException("DescriptionFr exceeds maximum length of 50 characters.");

			if (en is not null && en.Length > 50)
				throw new ArgumentException("DescriptionEn exceeds maximum length of 50 characters.");

			DescriptionFr = fr;
			DescriptionEn = en;
		}

		/// <summary>
		/// Modifie les champs éditables (ne modifie jamais TypeCode/Code).
		/// Applique les mêmes règles de normalisation et mise à jour audit.
		/// </summary>
		public void ModifyFields(string descriptionFr, string? descriptionEn, bool isActive)
		{
			SetDescriptions(descriptionFr, descriptionEn);
			IsActive = isActive;

			ModificationDate = DateTime.UtcNow;
		}

		#endregion
	}
}
