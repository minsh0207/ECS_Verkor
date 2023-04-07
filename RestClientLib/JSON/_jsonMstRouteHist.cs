using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_route_hist Request
    /// </summary>
    public class _jsonMstRouteHistRequest : __baseRequest
    {
        public _jsonMstRouteHistRequest() { }
        ~_jsonMstRouteHistRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_route_hist Response
    /// </summary>
    public class _jsonMstRouteHistResponse : _baseResponse
    {
        public _jsonMstRouteHistResponse() { }
        ~_jsonMstRouteHistResponse() { }

        public List<_mst_route_hist> DATA;
    }

}
