using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_tray_proc Request
    /// </summary>
    public class _jsonDatTrayProcRequest : __baseRequest
    {
        public _jsonDatTrayProcRequest() { }
        ~_jsonDatTrayProcRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_dat_tray_proc Response
    /// </summary>
    public class _jsonDatTrayProcResponse : _baseResponse
    {
        public _jsonDatTrayProcResponse() { }
        ~_jsonDatTrayProcResponse() { }

        public List<_dat_tray_proc> DATA;
    }

}
