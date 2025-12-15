// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 ICdSetService.cs
// Type de fichier                Interface
// Classe                         ICdSetService
// Emplacement                    IServices/SystemTables
// Entités concernées             CdSet
// Créé le                        2025-12-07
//
// Description
//     Interface de service définissant les opérations de consultation et
//     de transformation associées aux codes du système (CdSet). Fournit
//     notamment des utilitaires pour les listes déroulantes, la validation,
//     et l'affichage bilingue.
//
// Fonctionnalité
//     - Récupération des CdSet d'un type donné.
//     - Lecture d'un CdSet précis (TypeCode + Code).
//     - Vérification d'existence.
//     - Extraction de descriptions FR/EN.
//     - Génération de dictionnaires clé-valeur.
//     - Création, modification et désactivation.
//     - Désactivation complète d’une famille (TypeCode).
//     - Réactivation complète d’une famille (TypeCode).
//
// Modifications
//     2025-12-15    Ajout ReactivateFamilyAsync (réactivation logique par TypeCode).
//     2025-12-15    Ajout DeactivateFamilyAsync (désactivation logique par TypeCode).
//     2025-12-07    Version DRD v10 finale, méthode SelectListItem commentée
//                   temporairement jusqu'à l'intégration des composants UI.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DRD.Application.IServices.SystemTables
{
	public interface ICdSetService
	{
		#region Lecture

		Task<IEnumerable<CdSet>> GetAllAsync();

		Task<IEnumerable<CdSet>> GetByTypeCodeAsync(string typeCode);

		Task<CdSet?> GetByTypeCodeAndCodeAsync(string typeCode, string code);

		Task<bool> ExistsAsync(string typeCode, string code);

		Task<string> GetDescriptionAsync(
			string typeCode,
			string code,
			string? culture = null);

		Task<Dictionary<string, string>> GetCdSetKeyValueListAsync(
			string typeCode,
			string? culture = null);

		#endregion

		#region Écriture / État

		Task CreateAsync(CdSet entity);

		Task UpdateAsync(CdSet entity);

		Task DeactivateAsync(CdSet entity);

		Task<int> DeactivateFamilyAsync(string typeCode);

		Task<int> ReactivateFamilyAsync(string typeCode);

		#endregion

		// ❗ À réactiver lorsque les builders et dropdowns seront migrés en v10
		/*
        Task<List<SelectListItem>> GetCdSetSelectListAsync(
            string typeCode,
            string? selectedValue = null,
            string? culture = null);
        */
	}
}
