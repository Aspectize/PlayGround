using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Aspectize.Core;

namespace PlayGround
{
    public interface IDataService
    {
        [Command(IsSaveCommand = true)]
        DataSet SaveData(DataSet dataSet, string sessionId, string circulatingId, bool updateVersion);

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

        DataSet IDataService.SaveData(DataSet dataSet, string sessionId, string circulatingId, bool updateVersion)
        {
            IDataManager dm = EntityManager.FromDataSetAndBaseService(dataSet, "MyDataService");

            IEntityManager em = dm as IEntityManager;

            var log = new AspectizeTaskLoggingHelper();

            var currentSessionId = circulatingId;

            var appName = "PlayGroundRT";

            Session session = null;

            var sessions = em.GetAllInstances<Session>();

            if (sessions.Count > 0)
            {
                session = sessions[0];
            }
            else {
                session = dm.GetEntity<Session>(circulatingId);
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

            IFileService fs = ExecutingContext.GetService<IFileService>("MyFileService");

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

            IEntityManager emResult = EntityManager.FromDataSet(DataSetHelper.Create());

            Session sessionResult = emResult.CreateInstance<Session>();

            sessionResult.Bindings = session.Bindings;
            sessionResult.CirculatingId = session.CirculatingId;
            sessionResult.css = session.css;
            sessionResult.Html = session.Html;
            sessionResult.HtmlControlInfoJSON = session.HtmlControlInfoJSON;
            sessionResult.Id = sessionId;
            sessionResult.js = session.js;
            sessionResult.JSLibrary = session.JSLibrary;
            sessionResult.JSView = session.JSView;
            sessionResult.Log = session.Log;
            sessionResult.Persist = session.Persist;

            emResult.Data.AcceptChanges();

            return emResult.Data;
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
            dm.LoadEntities<Session>();

            var ids = new Dictionary<string, string>();

            ids.Add("a6f804a44", "ComboBox basic");
            ids.Add("3fc7b2d3c", "ComboBox with Null Value");
            ids.Add("d6627c3c6", "ComboBox with parent child value");
            ids.Add("de18b5b35", "ComboBox with Selectedvalue on Relation");
            ids.Add("d74c07320", "RadioButton basic");
            ids.Add("f76a37b51", "RadioButton with SelectedValue on Relation");
            ids.Add("95da0760f", "Basic Image");
            ids.Add("14f82140a", "Basic Repeater");
            ids.Add("0fc0f1843", "Repeater with order and filter");


            foreach (KeyValuePair<string, string> kvp in ids)
            {
                if (!em.GetAllInstances<EnumInBaseSample>().Exists(item => item.Id == kvp.Key))
                {
                    EnumInBaseSample e = em.CreateEnumInstance<EnumInBaseSample>();
                    e.Id = kvp.Key;
                    e.Name = kvp.Value;
                }
            }

            foreach(Session session in em.GetAllInstances<Session>())
            {
                if (!ids.ContainsKey(session.Id))
                {
                    session.Delete();

                }

            }

            dm.SaveTransactional();

        }

        //void IDataService.SaveInfo(string html, string css, string javascript, string bindings)
        //{
            
        //}

    }

}
