using DRD.Web.Models.ConfigurationTables;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Globalization;
using System.Resources;
using DRD.Resources;

namespace DRD.Web.Controllers.Helpers
{
    public static class CdSetHtmlHelper
    {
        public static IHtmlContent CdSetDropdown(this IHtmlHelper html, CdSetSelectVM model, ResourceManager? resourceManager = null)
        {
            var group = new TagBuilder("div");
            group.AddCssClass("form-group");

            // Résolution du libellé depuis les ressources
            var labelText = resourceManager?.GetString(model.FieldName, CultureInfo.CurrentUICulture)
                            ?? model.Label ?? model.FieldName;

            var label = new TagBuilder("label");
            label.Attributes.Add("for", model.FieldName);
            label.InnerHtml.Append(labelText);
            group.InnerHtml.AppendHtml(label);

            // Création du <select>
            var select = new TagBuilder("select");
            select.Attributes.Add("id", model.FieldName);
            select.Attributes.Add("name", model.FieldName);
            select.AddCssClass("form-control select-3d");

            // Option par défaut
            var defaultOption = new TagBuilder("option");
            defaultOption.Attributes["value"] = "";
            defaultOption.InnerHtml.Append("-- " + Common.Select_DefaultOption + " --");
            select.InnerHtml.AppendHtml(defaultOption);

            foreach (var item in model.Items)
            {
                var option = new TagBuilder("option");
                option.Attributes["value"] = item.Value;
                if (item.Selected) option.Attributes["selected"] = "selected";
                option.InnerHtml.Append(item.Text);
                select.InnerHtml.AppendHtml(option);
            }

            group.InnerHtml.AppendHtml(select);

            // Validation
            var span = new TagBuilder("span");
            span.AddCssClass("text-danger");
            span.Attributes.Add("data-valmsg-for", model.FieldName);
            span.Attributes.Add("data-valmsg-replace", "true");
            group.InnerHtml.AppendHtml(span);

            return group;
        }
    }
}
