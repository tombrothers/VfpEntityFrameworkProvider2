using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NorthwindEFModel
{
    // By putting our method stubs in this file, we won't loose them if we regenerate the model from the DB.
    public partial class NorthwindEntities : ObjectContext
    {
        // This method stub maps the GetProductsForCategory method to the NorthwindEFModel.GetProductsForCategory model defined function
        // Because we want to expose the function as a method on an ObjectContext object, we need to provide bootstrapping code.
        [DbFunction("NorthwindEFModel", "GetProductsForCategory")]
        public IQueryable<Product> GetProductsForCategory(string categoryName)
        {
            return this.QueryProvider.CreateQuery<Product>(
                Expression.Call(
                    Expression.Constant(this),
                    (MethodInfo)MethodInfo.GetCurrentMethod(),
                    Expression.Constant(categoryName, typeof(string))
                    ));
        }
    }

    // We can also expose our mapped methods on a static class.
    public static class NorthwindEntitiesExtensions
    {
        // This method stub maps the TimesMDF method to the NorthwindEFModel.TimesMDF model defined function
        // We're exposing it as a static method, so we don't need the bootstrapping code. We just throw an exception 
        // so the method will compile.
        [DbFunction("NorthwindEFModel", "TimesMDF")]
        public static int TimesMDF(int x, int y)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
    }
}
