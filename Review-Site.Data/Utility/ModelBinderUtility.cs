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
    }
}
