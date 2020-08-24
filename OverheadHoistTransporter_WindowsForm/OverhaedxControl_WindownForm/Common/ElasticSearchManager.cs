using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.Common
{
    public class ElasticSearchManager
    {

        public const string ELASTIC_URL = "elastic.viewer.mirle.com.tw";
        public const string ELASTIC_TABLE_INDEX_SYSEXCUTEQUALITY = "mfoht100-ohtc1-sysexcutequality";
        public const string ELASTIC_TABLE_INDEX_RECODEREPORTINFO = "mfoht100-ohtc1-recodereportinfo";



        public List<T> Search<T>(string url, string search_table_index, DateRangeQuery dq, TermsQuery[] tsqs, string[] includes_column, int start_index, int each_search_size)
            where T : class
        {
            var node = new Uri($"http://{url}:9200");
            var settings = new ConnectionSettings(node).DefaultIndex("default");
            settings.DisableDirectStreaming();
            var client = new ElasticClient(settings);
            SearchRequest sr = new SearchRequest($"{search_table_index}*");
            sr.From = start_index;
            sr.Size = each_search_size;

            if (tsqs != null)
            {
                foreach (var tsq in tsqs)
                {
                    if (tsq != null)
                        sr.Query &= tsq;
                }
            }
            sr.Query &= dq;
            sr.Source = new SourceFilter()
            {
                Includes = includes_column,
            };
            var result = client.Search<T>(sr);
            return result.Documents.ToList();
        }

        public List<T> Search<T>(string url, string search_table_index, DateRangeQuery dq, TermsQuery[] tsqs, int start_index, int each_search_size)
           where T : class
        {
            var node = new Uri($"http://{url}:9200");
            var settings = new ConnectionSettings(node).DefaultIndex("default");
            settings.DisableDirectStreaming();
            var client = new ElasticClient(settings);
            SearchRequest sr = new SearchRequest($"{search_table_index}*");
            sr.From = start_index;
            sr.Size = each_search_size;
            var tmpPropertiesAry = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);



            if (tsqs != null)
            {
                foreach (var tsq in tsqs)
                {
                    if (tsq != null)
                        sr.Query &= tsq;
                }
            }
            sr.Query &= dq;
            sr.Source = new SourceFilter()
            {
                Includes = tmpPropertiesAry,
            };
            var result = client.Search<T>(sr);
            return result.Documents.ToList();
        }
    }
}
