using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_tray Request
    /// </summary>
    public class _jsonDatTrayRequest : __baseRequest
    {
        public _jsonDatTrayRequest() { }
        ~_jsonDatTrayRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_dat_tray Response
    /// </summary>
    public class _jsonDatTrayResponse : _baseResponse
    {
        public _jsonDatTrayResponse() { }
        ~_jsonDatTrayResponse() { }

        public List<_dat_tray> DATA;
    }

}
