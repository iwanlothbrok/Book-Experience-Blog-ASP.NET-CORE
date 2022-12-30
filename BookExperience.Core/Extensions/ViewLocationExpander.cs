﻿namespace BookExperience.Core.Extensions
{
    using Microsoft.AspNetCore.Mvc.Razor;
    using System.Collections.Generic;
    using System.Linq;

    public class ViewLocationExpander : IViewLocationExpander
    {
        /// <summary>
        /// Used to specify the locations that the view engine should search to 
        /// locate views.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewLocations"></param>
        /// <returns></returns>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //{2} is area, {1} is controller,{0} is the action
            string[] locations = new string[] { "/Pages/{2}/{1}/{0}.cshtml" };
            return locations.Union(viewLocations);          //Add mvc default locations after ours
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(ViewLocationExpander);
        }
    }
}