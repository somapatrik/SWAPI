using System.Collections.Specialized;
using System.Web;

namespace SWAPI.Services
{
    internal class RequestBuilder
    {
        private string _BaseUrl = "https://swapi.dev/api/planets";
        private NameValueCollection parameters = new NameValueCollection();

        private string _Url;
        public string Url
        {
            get
            {
                BuildUrl();
                return _Url;
            }
            private set
            {
                _Url = value;
            }
        }

        public RequestBuilder(int page)
        {
            AddParameter("page", page.ToString());
        }

        public RequestBuilder(int page, string searchName)
        {
            AddParameter("search", searchName);
            AddParameter("page", page.ToString());
        }

        private void AddParameter(string key, string value)
        {
            parameters.Add(key, value);
        }

        private void RemoveParameter(string key)
        {
            parameters.Remove(key);
        }

        private void BuildUrl()
        {
            UriBuilder uriBuilder = new UriBuilder(_BaseUrl);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (string key in parameters.AllKeys)
            {
                queryString[key] = parameters[key];
            }

            uriBuilder.Query = queryString.ToString();

            Url = uriBuilder.ToString();
        }

    }
}
