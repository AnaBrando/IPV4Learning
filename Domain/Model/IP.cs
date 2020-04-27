using System;

namespace Domain.Model
{
    public class IP
    {
        public int id { get; set; }
        public string ip { get; set; }
        public string ipBinary { get; set; }
        public string maskBinary { get; set; }
        public string networkBinary { get; set; }
        public string CDIR { get; set; }
        public override bool Equals(Object obj)
        {
            if (obj is IP)
            {
                var that = obj as IP;
                return this.id == that.id && this.ip == that.ip;
            }

            return false;
        }

     

    }

}
