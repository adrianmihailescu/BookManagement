using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.DynamicData;

using System.Linq.Expressions;

namespace BookManagement.DynamicData.Filters
{

    public partial class TextFilter : System.Web.DynamicData.QueryableFilterUserControl
    {
        
        
        //////////////////
        private const string NullValueString = "[null]";


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_OnTextChanged(object sender, EventArgs e)
        {
            OnFilterChanged();
        }

        public override IQueryable GetQueryable(IQueryable source)
        {
            if (string.IsNullOrEmpty(this.txtFilter1.Text))
              {
                return source;
              } 

              string date = this.txtFilter1.Text;
              ConstantExpression value = Expression.Constant(date); 

              ParameterExpression parameter = Expression.Parameter(source.ElementType);
              MemberExpression property = Expression.Property(parameter, this.Column.Name);
              if (Nullable.GetUnderlyingType(property.Type) != null)
              {
                property = Expression.Property(property, "Text");
              } 

              Expression comparison;

              comparison = Expression.Call(property, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), value);


              LambdaExpression lambda = Expression.Lambda(comparison, parameter); 

              MethodCallExpression where = Expression.Call(
                typeof(Queryable)
                , "Where"
                , new Type[] { source.ElementType }
                , new Expression[] { source.Expression, Expression.Quote(lambda) }); 

              return source.Provider.CreateQuery(where);
        }
    }
}