using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_route_order Request
    /// </summary>
    public class _jsonMstRouteOrderRequest : __baseRequest
    {
        public _jsonMstRouteOrderRequest() { }
        ~_jsonMstRouteOrderRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_route_order Response
    /// </summary>
    public class _jsonMstRouteOrderResponse : _baseResponse
    {
        public _jsonMstRouteOrderResponse() { }
        ~_jsonMstRouteOrderResponse() { }

        public List<_mst_route_order> DATA;
    }

}
