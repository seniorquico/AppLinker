using System.Collections.Generic;
using System.Reflection;
using AppLinker.Api.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace AppLinker.Api.Options
{
    public sealed class AppLinksControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly bool enabled;

        public AppLinksControllerFeatureProvider(bool enabled) =>
            this.enabled = enabled;

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            if (!this.enabled)
            {
                var appLinksControllerType = typeof(AppLinksController);
                TypeInfo targetControllerType = null;
                foreach (var controllerType in feature.Controllers)
                {
                    if (appLinksControllerType.Equals(controllerType))
                    {
                        targetControllerType = controllerType;
                        break;
                    }
                }

                if (targetControllerType != null)
                {
                    feature.Controllers.Remove(targetControllerType);
                }
            }
        }
    }
}
