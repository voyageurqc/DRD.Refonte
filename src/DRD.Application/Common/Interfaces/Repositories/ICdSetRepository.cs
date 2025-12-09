// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 ICdSetRepository.cs
// Type de fichier                Interface
// Classe                         ICdSetRepository
// Emplacement                    Common/Interfaces/Repositories
// Entités concernées             CdSet
// Créé le                        2025-12-07
//
// Description
//     Interface spécialisée pour la gestion des entités CdSet. Fournit des
//     opérations de consultation et de suppression basées sur la clé composite
//     TypeCode + Code, en complément des opérations génériques.
//
// Fonctionnalité
//     - Récupération de tous les CdSet pour un TypeCode donné.
//     - Récupération d'un CdSet précis (TypeCode + Code).
//     - Suppression d'un enregistrement spécifique CdSet.
//     - Héritage du repository générique pour les opérations standard.
//
// Modifications
//     2025-12-07    Version DRD v10 finale. Alignement avec CdSet (v10) et
//                   IGenericRepository v10. Migration à partir de v9.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;

namespace DRD.Application.Common.Interfaces.Repositories
{
	/// <summary>
	/// Repository spécialisé pour l'entité CdSet.
	/// Fournit les méthodes supplémentaires nécessaires à la gestion
	/// des codes par catégorie (TypeCode).
	/// </summary>
	public interface ICdSetRepository : IGenericRepository<CdSet>
	{
		/// <summary>
		/// Retourne tous les CdSet appartenant à un TypeCode donné.
		/// </summary>
		Task<IEnumerable<CdSet>> GetByTypeCodeAsync(string typeCode);

		/// <summary>
		/// Retourne un CdSet précis basé sur la clé composite TypeCode + Code.
		/// </summary>
		Task<CdSet?> GetByTypeCodeAndCodeAsync(string typeCode, string code);
        /// <summary>
        /// Vérifie si un CdSet existe pour une clé composite (TypeCode + Code).
        /// </summary>
        Task<bool> ExistsAsync(string typeCode, string code);


        /// <summary>
        /// Supprime physiquement un CdSet via sa clé composite.
        /// </summary>
        Task DeleteCdSetAsync(string typeCode, string code);
	}
}
