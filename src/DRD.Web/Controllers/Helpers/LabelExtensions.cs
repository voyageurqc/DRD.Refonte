using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DRD.Web.Controllers.Helpers
{
    public static class LabelExtensions
    {
        public static IHtmlContent RequiredLabelFor<TModel, TValue>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string? labelText = null)
        {
            // Get the property name
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("Expression must be a member expression");

            var propertyName = memberExpression.Member.Name;

            // Get metadata for the property
            var metadata = htmlHelper.ViewData.ModelMetadata.Properties
                .FirstOrDefault(p => p.PropertyName == propertyName);

            bool required = metadata != null && metadata.ValidatorMetadata.Any(v => v is RequiredAttribute);

            var label = htmlHelper.LabelFor(expression, labelText, new { @class = "control-label" }).GetString();
            if (required)
            {
                label = $"<span class=\"text-danger\">*</span> {label}";
            }
            return new HtmlString(label);
        }

        // Helper to get string from IHtmlContent
        private static string GetString(this IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}