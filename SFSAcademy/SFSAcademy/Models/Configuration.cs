using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFSAcademy.Models
{
    public class Configuration
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();
        // To Do : Needs to identify this and based on that update the below query
        //

        //STUDENT_ATTENDANCE_TYPE_OPTIONS = [["#{t('daily_text')}", "Daily"], ["#{t('subject_wise_text')}", "SubjectWise"]]

        //  NETWORK_STATES                   = [["#{t('online')}",'Online'],["#{t('offline')}",'Offline']]
        //  LOCALES = []
        //        Dir.glob("#{RAILS_ROOT}/config/locales/*.yml").each do |file|
        //    file.gsub!("#{RAILS_ROOT}/config/locales/", '')
        //    file.gsub!(".yml", '')
        //    LOCALES << file

        public Dictionary<string, string> STUDENT_ATTENDANCE_TYPE_OPTIONS { get; set; }
        public Dictionary<string, string> NETWORK_STATES { get; set; }
        public string[] LOCALES { get; set; }
        public string config_key { get; set; }
        public string config_value { get; set; }
        public string errorMessage { get; set; }

        public Configuration()
        {
            STUDENT_ATTENDANCE_TYPE_OPTIONS.Add("daily_text", "Daily");
            STUDENT_ATTENDANCE_TYPE_OPTIONS.Add("subject_wise_text", "SubjectWise");
            NETWORK_STATES.Add("online", "Online");
            NETWORK_STATES.Add("offline", "Offline");

            errorMessage = validate(this.errorMessage);
            get_config_value(this.config_key);


        }

        public string validate(string key)
        {
            string finalmessage = string.Empty;
            string studenterrorMessage = "student_attendance_type_should_be_one";
            string networkerrorMessage = "student_attendance_type_should_be_one";
            if (key == "StudentAttendanceType")
            {
                if(!STUDENT_ATTENDANCE_TYPE_OPTIONS.ContainsKey(key))
                {
                    string studentKeyValue = string.Empty;
                    foreach (var studentKey in STUDENT_ATTENDANCE_TYPE_OPTIONS)
                    {
                        studentKeyValue = studentKeyValue + studentKey.Value;
                    }
                    studenterrorMessage = "student_attendance_type_should_be_one" + studentKeyValue;
                }
                finalmessage = studenterrorMessage;
            }
            if (key == "NetworkState")
            {
                if (!NETWORK_STATES.ContainsKey(key))
                {
                    string networkState = string.Empty;
                    foreach (var networkKey in NETWORK_STATES)
                    {
                        networkState = networkState + networkKey.Value;
                    }
                    networkerrorMessage = "student_attendance_type_should_be_one" + networkState;
                }
                finalmessage= networkerrorMessage;
            }

            return finalmessage;
        }

        public void clear_school_cache(User user)
        {
            //To Do... Delete User From Cache
        }

        public string get_config_value(string key)
        {
            var res = db.CONFIGURATIONs.Find(key);
            return (res == null) ? null : res.CONFIG_VAL;
        }

        // To Do... Needs to create Table and based on that implement logic

        //public bool save_institution_logo(Upload upload)
        //{
        //    directory, filename = "#{RAILS_ROOT}/public/uploads/image", 'institute_logo.jpg'
        //    path = File.join(directory, filename) # create the file path
        //    File.open(path, "wb") { | f | f.write(upload['datafile'].read) } # write the file
        //}

        // To Do... Needs to create Module Table and Add key in Config Table and then based on that implement logic
        //public Module available_modules(string key)
        //{
        //    var res = db.CONFIGURATIONs.Find(key);
        //    var moduleDetails = db.Module.Fins(res.CONFIG_VAL);
        //}

        public void set_config_values(Dictionary<string,string> config)
        {
            foreach (var conf in config)
            {
                set_value(conf.Key, conf.Value);
            }
        }

        public void set_value(string key,string value)
        {
            var config = db.CONFIGURATIONs.Find(key);
            if (config == null)
            {
                CONFIGURATION conf = new CONFIGURATION();
                conf.CONFIG_KEY = key;
                conf.CONFIG_VAL = value;
                db.CONFIGURATIONs.Add(conf);
                db.SaveChanges();
            }
        }

        public Dictionary<string,string> get_multiple_configs_as_hash(string[] keys)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach (var key in keys)
            {
                res.Add(key,get_config_value(key));
            }
            return res;
        }
    }
}