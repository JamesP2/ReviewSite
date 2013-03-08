using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Review_Site.Data.Utility
{
    internal static class ModelBinderUtility
    {
        /// <summary>
        /// Remove errors from a key in the ModelState. Neatens up ModelBinder code whilst still avoiding the NullReferenceException here.
        /// </summary>
        /// <param name="context">The context containing the ModelState</param>
        /// <param name="key">The key to remove.</param>
        internal static void ClearErrors(ModelBindingContext context, string key)
        {
            if (context.ModelState.ContainsKey(key)) context.ModelState[key].Errors.Clear();
        }

        internal static void RebuildModelState(ControllerContext controllerContext, ModelBindingContext bindingContext, object model)
        {
            //bindingContext.ModelState.Clear();
            foreach (string key in bindingContext.ModelState.Keys)
            {
                bindingContext.ModelState[key].Errors.Clear();
            }
            foreach (ModelMetadata property in bindingContext.PropertyMetadata.Values)
            {
                property.Model = bindingContext.ModelType.GetProperty(property.PropertyName).GetValue(model, null);

                foreach (ModelValidator validator in property.GetValidators(controllerContext))
                {
                    foreach (ModelValidationResult result in validator.Validate(model)) 
                        bindingContext.ModelState.AddModelError(property.PropertyName, result.Message);
                }
            }
        }
    }
}
