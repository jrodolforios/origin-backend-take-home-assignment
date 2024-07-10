using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Diagnostics.CodeAnalysis;

namespace Dotnet.OriginAssignment.Api.Middlewares
{

    [ExcludeFromCodeCoverage]
    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _centralPrefix;

        public RoutePrefixConvention()
        {
            _centralPrefix = new AttributeRouteModel(new RouteAttribute("api"));
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var selectorModel = controller.Selectors.FirstOrDefault();
                if (selectorModel != null)
                {
                    selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix, selectorModel.AttributeRouteModel);
                }
            }
        }
    }
}
