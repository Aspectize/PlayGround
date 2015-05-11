using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Aspectize.Core;

namespace PlayGround.Services
{
    public interface IDataService
    {
        [Command(IsSaveCommand = true)]
        DataSet SaveData(DataSet dataSet, string sessionId, bool updateVersion);

        DataSet LoadSession(string sessionId);

        DataSet LoadSamples();

        void PrepareSamples();
    }

    [Service(Name = "DataService")]
    public class DataService : IDataService //, IInitializable, ISingleton
    {

        DataSet IDataService.LoadSamples()
        {
            IDataManager dm = EntityManager.FromDataBaseService("MyDataService");

            dm.LoadDatabaseEnum<EnumInBaseSample>();

            return dm.Data;
        }   

        DataSet IDataService.SaveData(DataSet dataSet, string sessionId, bool updateVersion)
        {
            IDataManager dm = EntityManager.FromDataSetAndBaseService(dataSet, "MyDataService");

            IEntityManager em = dm as IEntityManager;

            var log = new AspectizeTaskLoggingHelper();

            var currentSessionId = sessionId;

            var appName = "PlayGroundRT";

            Session session = null;

            var sessions = em.GetAllInstances<Session>();

            if (sessions.Count > 0)
            {
                session = sessions[0];
            }
            else {
                session = dm.GetEntity<Session>(sessionId);
            }

            NextId nextId = null;

            if (updateVersion) {

                if (currentSessionId.Contains("-"))
                {
                    var parts = currentSessionId.Split('-');

                    nextId = dm.GetEntity<NextId>(parts[0]);
                    nextId.Version++;
                }
                else
                {
                    nextId = em.CreateInstance<NextId>();

                    nextId.Id = session.Id;
                    nextId.Version = 1;
                }

                currentSessionId = string.Format("{0}-{1}", nextId.Id, nextId.Version);

                session.Id = currentSessionId;
                session.Persist = true;

                session.data.AcceptChanges();
                session.data.SetAdded();
            }

            var controlInfos = WebBuilder.BuildHtml(appName, session.Html, session.JSLibrary, log);

            string jsonControlInfos = JsonSerializer.Serialize(controlInfos);

            session.HtmlControlInfoJSON = jsonControlInfos;

            //AspectizeListDictionary<string, string> dicoContext = new AspectizeListDictionary<string, string>();

            //dicoContext.Add("AdventureWorksData", "PLayGroundRT/AdventureWorks.HumanResources");
            //dicoContext.Add("AdventureWorksData", "PLayGroundRT/AdventureWorks.Person");
            //dicoContext.Add("AdventureWorksData", "PLayGroundRT/AdventureWorks.Production");
            //dicoContext.Add("AdventureWorksData", "PLayGroundRT/AdventureWorks.Purchasing");
            //dicoContext.Add("AdventureWorksData", "PLayGroundRT/AdventureWorks.Sales");

            //var typeInfo = WebIntellisenseCompiler.getTypeInfo(typesFilePath);

            IFileService fs = ExecutingContext.GetService<IFileService>("MyFileService");

            //var typeInfo = fs.ReadBytes("PlayGroundRT.Types.Intellisense.js");
            var typeInfo = fs.Read("PlayGroundRT.Types.Intellisense.txt");

            System.IO.StreamReader reader = new System.IO.StreamReader(typeInfo);
            string text = reader.ReadToEnd();

            //var typeInfoString = System.Text.Encoding.UTF8.GetString(text);

            var ctrinfos = WebBuilder.BuildTypeDefinitionControl(appName, controlInfos);

            var index = text.IndexOf("aas:'Controls'") + "aas:'Controls'".Length;

            string temp = text.Substring(0, index) + ctrinfos + text.Substring(index);

            var jsView = WebBuilder.BuildViews(appName, session.Bindings, temp, log);

            session.JSView = jsView;

            var jsLibrary = WebBuilder.BuildJSLibrary(appName, session.js, log);

            session.JSLibrary = jsLibrary;
            session.Log = log.ToString();
            session.CirculatingId = currentSessionId;

            dm.SaveTransactional();

            //if (updateVersion)
            //{
            //    //var x = System.Web.HttpContext.Current.Request.Url;
            //    //ExecutingContext.RedirectUrl = 

            //    //var url = "http://localhost/WebHost/PlayGround/id=250d4152b-1";
            //    //System.Web.HttpContext.Current.Request.Redirect(url, false);
            //}

            //return currentSessionId;
            return dm.Data;
        }

        DataSet IDataService.LoadSession(string sessionId)
        {
            IDataManager dm = EntityManager.FromDataBaseService("MyDataService");

            var session = dm.GetEntity<Session>(sessionId);

            session.CirculatingId = sessionId;

            dm.Data.AcceptChanges();

            return dm.Data;
        }

        void IDataService.PrepareSamples()
        {
            IDataManager dm = EntityManager.FromDataBaseService("MyDataService");

            IEntityManager em = dm as IEntityManager;

            dm.LoadDatabaseEnum<EnumInBaseSample>();

            var ids = new Dictionary<string, string>();

            ids.Add("a6f804a44", "ComboBox basic");

            foreach (KeyValuePair<string, string> kvp in ids)
            {
                if (!em.GetAllInstances<EnumInBaseSample>().Exists(item => item.Id == kvp.Key))
                {
                    EnumInBaseSample e = em.CreateEnumInstance<EnumInBaseSample>();
                    e.Id = kvp.Key;
                    e.Name = kvp.Value;
                }
            }

            dm.SaveTransactional();

        }

        //void IDataService.SaveInfo(string html, string css, string javascript, string bindings)
        //{
            
        //}

    }

}