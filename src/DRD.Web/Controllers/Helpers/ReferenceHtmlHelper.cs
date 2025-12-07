using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Resources;

namespace DRD.Web.Controllers.Helpers
{
    public static class ReferenceHtmlHelper
    {
        public static IHtmlContent ReferenceDropdown(
            this IHtmlHelper html,
            string fieldName,
            string? selectedValue,
            List<SelectListItem> items,
            ResourceManager resourceManager)
        {
            var label = resourceManager.GetString(fieldName) ?? fieldName;

            var dropdown = new TagBuilder("select");
            dropdown.Attributes["name"] = fieldName;
            dropdown.Attributes["id"] = fieldName;
            dropdown.AddCssClass("form-select");

            // Option par défaut
            var defaultOption = new TagBuilder("option");
            defaultOption.Attributes["value"] = "";
            defaultOption.InnerHtml.Append("-- Sélectionner --");
            dropdown.InnerHtml.AppendHtml(defaultOption);

            foreach (var item in items)
            {
                var option = new TagBuilder("option");
                option.Attributes["value"] = item.Value;
                if (item.Value == selectedValue)
                    option.Attributes["selected"] = "selected";
                option.InnerHtml.Append(item.Text);
                dropdown.InnerHtml.AppendHtml(option);
            }

            // Assemble le bloc final avec label + dropdown
            var htmlOutput = new HtmlContentBuilder()
                .AppendHtml($"<label for=\"{fieldName}\">{label}</label>")
                .AppendHtml(dropdown);

            return htmlOutput;
        }
    }
}
