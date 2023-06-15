using System.Text;

namespace URLShortenerAPI.Common.ASCII
{
    public class NonAsciiRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var routeValue) && routeValue is string)
            {
                var value = (string)routeValue;
                return Encoding.UTF8.GetByteCount(value) == value.Length;
            }

            return false;
        }
    }
}
