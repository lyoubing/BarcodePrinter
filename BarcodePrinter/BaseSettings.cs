using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BarcodePrinter
{
    public class BaseSettings
    {
        public const string FileName = "base.config";
        public string LabelPrinter { get; set; }
        public int LabelStyle { get; set; }
        public int BatchNumber { get; set; }
        public int LastMonth { get; set; }
        public int LastNumber { get; set; }
        public static BaseSettings GetDefault()
        {
            return new BaseSettings
            {
                BatchNumber = 10,
                LastMonth = 1,
                LastNumber = 0,
                LabelStyle = 0,
            };
        }
        public void Save()
        {
            var r = JsonConvert.SerializeObject(this);

            System.IO.File.WriteAllText(FileName, r);
        }
        public static BaseSettings Load()
        {
            if (!File.Exists(BaseSettings.FileName)) return BaseSettings.GetDefault();

            var config = File.ReadAllText(BaseSettings.FileName);
            var setting = JsonConvert.DeserializeObject<BaseSettings>(config);
            return setting;
        }
    }
}
