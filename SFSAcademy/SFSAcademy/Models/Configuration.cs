using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFSAcademy.Models
{
    public class Configuration
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();
        public string find_by_config_key(string Config_Key)
        {
            var Config_Value = (from Config in db.CONFIGURATIONs
                                 where Config.CONFIG_KEY == Config_Key
                                 select new {Val = Config.CONFIG_VAL}).ToList();

            return Config_Value.FirstOrDefault().Val.ToString();
        }
    }
}