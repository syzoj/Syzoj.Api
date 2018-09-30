using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Syzoj.Api.Problems
{
    public class ProblemsetManagerBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if(context.Metadata.ModelType == typeof(IProblemsetManager))
            {
                return new BinderTypeModelBinder(typeof(ProblemsetManagerBinder));
            }

            return null;
        }
    }
}