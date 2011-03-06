using System;
using System.Web.Mvc;

namespace SkillDesk.Web.Controllers
{
    public class ControllerBase : Controller
    {
        private void UpdateModelWithCustomBinder<TModel>(TModel model, string prefix, IModelBinder binder, string include, string exclude)
        {
            var modelType = typeof(TModel);
            var bindAttribute = new BindAttribute { Include = include, Exclude = exclude };
            var metadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, modelType);
            var bindingContext = new ModelBindingContext
            {
                ModelMetadata = metadata,
                ModelName = prefix,
                ModelState = ModelState,
                ValueProvider = ValueProvider, 
                PropertyFilter = bindAttribute.IsPropertyAllowed
            };
            binder.BindModel(ControllerContext, bindingContext);
            if (!ModelState.IsValid)
                throw new InvalidOperationException("Error binding " + modelType.FullName);
        }
    }
}