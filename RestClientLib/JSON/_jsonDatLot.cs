using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_lot Request
    /// </summary>
    public class _jsonDatLotRequest : __baseRequest
    {
        public _jsonDatLotRequest() { }
        ~_jsonDatLotRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_dat_lot Response
    /// </summary>
    public class _jsonDatLotResponse : _baseResponse
    {
        public _jsonDatLotResponse() { }
        ~_jsonDatLotResponse() { }

        public List<_dat_lot> DATA;
    }

}
