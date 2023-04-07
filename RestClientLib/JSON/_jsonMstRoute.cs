using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_route Request
    /// </summary>
    public class _jsonMstRouteRequest : __baseRequest
    {
        public _jsonMstRouteRequest() { }
        ~_jsonMstRouteRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_route Response
    /// </summary>
    public class _jsonMstRouteResponse : _baseResponse
    {
        public _jsonMstRouteResponse() { }
        ~_jsonMstRouteResponse() { }

        public List<_mst_route> DATA;
    }

}
