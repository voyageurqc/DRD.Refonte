// ============================================================================
// 💻 Projet : DRD.Web
// 📄 Nom du fichier : wwwroot/js/components/simple-editor.js
// 🧩 Type (Rôle) : UI Script
// 🏷️ Classe (.NET) : JavaScript Asset
// 📍 Emplacement : DRD.Web/wwwroot/js/components/
// 🏛️ Entité(s) touchée(s) : WebMessage
// 🗓️ Créé le : 2025-10-28
// 📝 Description : Fonctions d'édition (wrap, lien) et prévisualisation (modal Bootstrap).
// 🔧 Fonctionnalités : Sans dépendances externes, compatible Bootstrap 5.
// 🧾 Modifications : 2025-10-28 : Création initiale.
// ============================================================================
/* global bootstrap */
(function () {
    function byId(id) { return document.getElementById(id); }


    window.SE_wrapText = function (id, openTag, closeTag) {
        const el = byId(id); if (!el) return;
        const start = el.selectionStart ?? 0;
        const end = el.selectionEnd ?? 0;
        const before = el.value.substring(0, start);
        const middle = el.value.substring(start, end);
        const after = el.value.substring(end);
        el.value = before + openTag + middle + closeTag + after;
        const pos = (before + openTag + middle + closeTag).length;
        el.setSelectionRange(pos, pos);
        el.focus();
    };


    window.SE_insertLink = function (id, promptText) {
        const url = window.prompt(promptText || 'URL:');
        if (!url) return;
        SE_wrapText(id, `<a href="${url}">`, '</a>');
    };


    // 👁️ Prévisualisation stylisée façon "WebMessage"
    window.SE_preview = function (textareaId, modalId) {
        const el = document.getElementById(textareaId);
        const container = document.getElementById(modalId + '_content');
        if (!el || !container) return;

        const content = el.value.trim() || "<em>Aucun contenu à afficher</em>";

        // Récupération des métadonnées
        let type = (el.dataset.type || 'INFO').toUpperCase();
        const mandatory = el.dataset.mandatory === 'true';

        // Dictionnaire couleurs DRD
        const TYPE_MAP = {
            ALERT: { cls: 'alert-danger', border: 'border-danger', icon: 'fa-triangle-exclamation' },
            WARN: { cls: 'alert-warning', border: 'border-warning', icon: 'fa-exclamation-circle' },
            DOWN: { cls: 'alert-dark text-white', border: 'border-dark', icon: 'fa-server' },
            ERROR: { cls: 'alert-danger', border: 'border-danger', icon: 'fa-bug' },
            INFO: { cls: 'alert-info', border: 'border-info', icon: 'fa-info-circle' },
            INT: { cls: 'alert-primary', border: 'border-primary', icon: 'fa-puzzle-piece' }
        };

        // INT obligatoire => danger
        const map = (type === 'INT' && mandatory) ? TYPE_MAP.ALERT : (TYPE_MAP[type] || TYPE_MAP.INFO);

        // HTML DRD Preview
        const previewHtml = `
        <div class="alert ${map.cls} shadow-sm rounded-3 p-4 border-start border-4 ${map.border}">
            <div class="d-flex align-items-center mb-2">
                <i class="fa-solid ${map.icon} me-2"></i>
                <strong class="fs-5">Aperçu du message</strong>
            </div>
            <div class="preview-body" style="line-height:1.5;">${content}</div>
            <div class="d-flex justify-content-end align-items-center mt-3 pt-2 border-top">
            </div>
        </div>`;

        container.innerHTML = previewHtml;
        new bootstrap.Modal(document.getElementById(modalId)).show();
    };
})();

