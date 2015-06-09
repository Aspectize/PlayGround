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
            string allTypesInfo = reader.ReadToEnd();

            //var typeInfoString = System.Text.Encoding.UTF8.GetString(text);

            var ctrinfos = WebBuilder.BuildTypeDefinitionControl(appName, controlInfos);

            var indexControls = allTypesInfo.IndexOf("aas:'Controls'") + "aas:'Controls'".Length;

            allTypesInfo = allTypesInfo.Substring(0, indexControls) + ctrinfos + allTypesInfo.Substring(indexControls);

            var javaScriptCommandInfos = WebTypeInfo.BuildJavascriptCommandInfo(session.js);

            var serviceInfos = WebBuilder.BuildTypeDefinitionBrowserServices(javaScriptCommandInfos);

            var indexBrowserServices = allTypesInfo.IndexOf("aas:'BrowserServices'") + "aas:'BrowserServices'".Length;

            allTypesInfo = allTypesInfo.Substring(0, indexBrowserServices) + serviceInfos + allTypesInfo.Substring(indexBrowserServices);

            var jsView = WebBuilder.BuildViews(appName, session.Bindings, allTypesInfo, log);

            session.JSView = jsView;

            var jsLibrary = WebBuilder.BuildJSLibrary(appName, session.js, log);

            session.JSLibrary = jsLibrary;
            session.Log = log.ToString();
            session.CirculatingId = currentSessionId;

            dm.SaveTransactional();

            //Create new Entity with same id for result

            IEntityManager emResult = EntityManager.FromDataSet(DataSetHelper.Create());

            Session sessionResult = emResult.CreateInstance<Session>();

            sessionResult.Id = sessionId;

            foreach (DataColumn dc in session.data.Table.Columns)
            {
                if (dc.ColumnName != PlayGround.Session.Fields.Id)
                {
                    sessionResult.data[dc.ColumnName] = session.data[dc.ColumnName];
                }
            }

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

            //ids.Add("a2a0da353", "ComboBox");
            //ids.Add("f936e220d", "ComboBox with Null Value");
            //ids.Add("b4f4373bb", "ComboBox with parent child value");
            //ids.Add("178b986bb", "ComboBox with Selectedvalue on Relation");
            //ids.Add("2dcd2191b", "RadioButton basic");
            //ids.Add("f76a37b51", "RadioButton with SelectedValue on Relation");
            //ids.Add("4b7a31fa6", "Image");
            //ids.Add("494d8dc36", "Repeater");
            //ids.Add("5ab74569a", "Repeater with order and filter");
            //ids.Add("1d2a34388", "Uploader");
            //ids.Add("0a3f22e0e", "Grid");
            //ids.Add("48ba1cc11", "TreeView");
            //ids.Add("27067ced4", "Panel");
            //ids.Add("d6efa71d7", "Tab");
            //ids.Add("f5cbe0590", "Tab Vertical");

            //ids.Add("b993a0fda", "Grid with Pagination and AutoSort");
            //ids.Add("6b0f48003", "Grid with Dynamic Filter");
            //ids.Add("5bde30cec", "Grid with Group Rows");
            //ids.Add("563cd3bf8", "Grid with different types of columns");
            //ids.Add("d6b568dc5", "Grid Parent Child");
            //ids.Add("18c465ab8", "Dialog");
            //ids.Add("89567a10c", "Flyout");
            //ids.Add("b70fa4ab4", "Boostrap Grid");
            //ids.Add("c9c59964f", "Bootstrap Tab");
            //ids.Add("2008273f6", "Bootstrap Modal");
            //ids.Add("33983fc4b", "Google Table");
            //ids.Add("4d3d85ff4", "Google Line Chart");
            //ids.Add("35e424ca9", "Hide or show if Data");
            //ids.Add("c665ff5ea", "Data Validation Demo");

            // en attente
            //ids.Add("a39fe7467", "JQuery DatePicker");
            //ids.Add("8767a7cbd", "Bind Data Demo");

            ids.Add("63145b6c5", "Bootstrap Modal");

            foreach (KeyValuePair<string, string> kvp in ids)
            {
                var session = em.GetInstance<Session>(kvp.Key);
                session.Persist = true;
                if (!em.GetAllInstances<EnumInBaseSample>().Exists(item => item.Id == kvp.Key))
                {
                    EnumInBaseSample e = em.CreateEnumInstance<EnumInBaseSample>();
                    e.Id = kvp.Key;
                    e.Name = kvp.Value;
                }
            }

            //foreach(Session session in em.GetAllInstances<Session>())
            //{
            //    if (!ids.ContainsKey(session.Id))
            //    {
            //        session.Delete();

            //    }

            //}

            dm.SaveTransactional();

        }

        //void IDataService.SaveInfo(string html, string css, string javascript, string bindings)
        //{
            
        //}

    }

}
