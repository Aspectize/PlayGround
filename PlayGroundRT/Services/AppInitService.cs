using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Aspectize.Core;
using Aspectize.Core.Web;

namespace PlayGround {
    public interface IAppInitService {

        Dictionary<string, string> GetVersionInfo ();

        IsJson LoadConfigSchema ([DefaultValueAttribute("Missing")] string versionKey);

        IsJson LoadConfigData ([DefaultValueAttribute("Missing")] string versionKey);

        IsJson LoadContextSchemas ([DefaultValueAttribute("Missing")] string versionKey);

        IsJson LoadControlTypeInfos ([DefaultValueAttribute("Missing")] string versionKey);

        IsJson LoadCommandInfos ([DefaultValueAttribute("Missing")] string versionKey);

        string LoadJsViews([DefaultValueAttribute("Missing")] string versionKey);
        string LoadJsLibrary([DefaultValueAttribute("Missing")] string versionKey);

    }

    [Service(Name = "AppInitService")]
    public class AppInitService : IAppInitService
    {
        static int version = 0;

        IWebInitService svc;

        Dictionary<string, object> dicoSessions = new Dictionary<string, object>();

        public AppInitService () {

            svc = ExecutingContext.GetService<IWebInitService>("WebInitService");

            version = 1 - version;
        }

        #region IAppInitService Members

        Dictionary<string, string> IAppInitService.GetVersionInfo () {

            return svc.GetVersionInfo();
        }

        IsJson IAppInitService.LoadConfigSchema (string versionKey) {

            return svc.LoadConfigSchema(versionKey);
        }

        IsJson IAppInitService.LoadConfigData (string versionKey) {

            return svc.LoadConfigData(versionKey);
        }

        IsJson IAppInitService.LoadContextSchemas (string versionKey) {

            return svc.LoadContextSchemas(versionKey);
        }

        IsJson IAppInitService.LoadControlTypeInfos (string versionKey) {

            var baseControls = svc.LoadControlTypeInfos(versionKey);

            // peut mieux faire !
            var baseControlInfo = baseControls.Value.Trim().Trim('[').Trim(']');

            var cachedSession = getSession(versionKey);

            var controlInfoJSonInfo = cachedSession.HtmlControlInfoJSON;

            var controlInfo = controlInfoJSonInfo.Trim().Trim('[').Trim(']');

            controlInfo = "[" + baseControlInfo + "," + controlInfo + "]";

            return new IsJson(controlInfo);
        }

        IsJson IAppInitService.LoadCommandInfos (string versionKey) {

            return svc.LoadCommandInfos(versionKey);
        }

        string IAppInitService.LoadJsViews(string versionKey)
        {

            //var data = System.IO.File.ReadAllText(@"D:\DriveS\Delivery\Applications\PlayGround\PlayGround.views.js");
            if (!string.IsNullOrEmpty(versionKey))
            {
                var cachedSession = getSession(versionKey);

                return cachedSession.JSView;
            }
            else
            {
                return "";
            }
        }

        string IAppInitService.LoadJsLibrary(string versionKey)
        {
            if (!string.IsNullOrEmpty(versionKey))
            {
                var cachedSession = getSession(versionKey);

                return cachedSession.JSLibrary;
            }
            else return "";

            //var data = System.IO.File.ReadAllText(@"D:\DriveS\Delivery\Applications\PlayGroundRT\PlayGroundRT.library.js");

            //return new IsJson(""); ;
        }

        private PLayGroundRT.Session getSession(string idSession)
        {
            if (!dicoSessions.ContainsKey(idSession))
            {
                IDataManager dm = EntityManager.FromDataBaseService("MyDataService");

                var session = dm.GetEntity<PLayGroundRT.Session>(idSession);

                dicoSessions.Add(idSession, session);
            }

            var cachedSession = (PLayGroundRT.Session)dicoSessions[idSession];
            return cachedSession;
        }

        #endregion
    }

}