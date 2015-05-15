/// <reference path="S:\Delivery\Aspectize.core\AspectizeIntellisense.js" />

Global.ClientService = {

   aasService:'ClientService',
   aasPublished: true,
   aasCommandAttributes: { GetSession: { CanExecuteOnStart: true } },
   MainData: 'MainData',

   ResizeAllEditors: function() {
       ace.edit('MainView-HTMLEditor').resize();
       ace.edit('MainView-CSSEditor').resize();
       ace.edit('MainView-JSEditor').resize();
       ace.edit('MainView-BindingEditor').resize();
   },

   GetSession: function (id) {

       if (id) {
           var em = Aspectize.EntityManagerFromContextDataName('MainData');

           var sessions = em.GetAllInstances('Session');

           if (sessions.length > 0) {
               em.ClearAllInstances('Session');
           }

           var cmd = Aspectize.Host.PrepareCommand();

           cmd.Attributes.aasAsynchronousCall = true;
           cmd.Attributes.aasShowWaiting = true;
           cmd.Attributes.aasDataName = this.MainData;
           cmd.Attributes.aasMergeData = true;

           var That = this;

           cmd.OnComplete = function (result) {
               //That.SetIFrameUrl('MainView-IFrameApplication', id);
               var em = Aspectize.EntityManagerFromContextDataName('MainData');

               var session = em.GetInstance('Session', { Id: id });

               initPanel(session);

               restoreResizable();

               That.DisplayResult(id, session.Log);
           };

           cmd.Call('Server/DataService.LoadSession', id);
       }
   },

   TogglePanel: function (name) {
       var em = Aspectize.EntityManagerFromContextDataName('MainData');

       var session = em.GetAllInstances('Session')[0];

       session.SetField(name, !session[name]);
       restoreResizable();
   },

   TogglePanelAndResize: function (name, panel) {

       var nbVisibleBefore = $('.TogglePanel').not('.hidden').length;

       if (nbVisibleBefore == 1 && !$('.' + panel).hasClass('hidden')) return;

       this.TogglePanel(name);
       var $items = $('.TogglePanel').not('.hidden');
       var nbVisible = $items.length;
       var totalWidth = $('.TopPanel').width();
       $items.each(function () {
           $(this).width((totalWidth / nbVisible) - 10);
       });
       restoreResizable();

       if (nbVisibleBefore == 1 && nbVisible == 2) {
           if (panel == 'HtmlPanel') {
               initResizablePanel(".HtmlPanel");
           } else if (panel == 'CssPanel') {
               if ($(".HtmlPanel").hasClass('hidden')) {
                   initResizablePanel(".CssPanel");
               } else {
                   initResizablePanel(".HtmlPanel");
               }
           } else if (panel == 'JsPanel') {
               if ($(".HtmlPanel").hasClass('hidden')) {
                   initResizablePanel(".CssPanel");
               } else {
                   initResizablePanel(".HtmlPanel");
               }
           }
       } else if (nbVisibleBefore == 2 && nbVisible == 1) {
           if (panel == 'HtmlPanel') {
           } else if (panel == 'CssPanel') {
               if (!$(".HtmlPanel").hasClass('hidden')) {
                   $(".HtmlPanel").resizable('destroy');
               }
           } else if (panel == 'JsPanel') {
               if (!$(".HtmlPanel").hasClass('hidden')) {
                   $(".HtmlPanel").resizable('destroy');
               } else if (!$(".CssPanel").hasClass('hidden')) {
                   $(".CssPanel").resizable('destroy');
               }
           }
       } else if (nbVisibleBefore == 3 && nbVisible == 2) {
           if (panel == 'JsPanel') {
               $(".CssPanel").resizable('destroy');
           }
       } else if (nbVisibleBefore == 2 && nbVisible == 3) {
           if (panel == 'JsPanel') {
               initResizablePanel(".CssPanel");
           } 
       }
   },

   InsertControlDefinition: function (editorName, controlName) {
       var control = $('#' + controlName);

       var ctrlName = control.attr('data-aasName');
       var ctrlType = control.attr('data-aasType');
       var html = "<div aas-name='" + ctrlName + "' aas-type='" + ctrlType + "'></div>";

       Aspectize.Host.ExecuteCommand('AceClientService.InsertCode', editorName, html);
   },

   Run: function(data, sessionId, updateVersion) {
       var em = Aspectize.EntityManagerFromContextDataName('MainData');

       var changes = em.GetDataSet().HasChanges();

       var session = em.GetInstance('Session', { Id: sessionId });

       if (session.Persist && !changes) return;

       var shouldUpdateVersion = (updateVersion == 'force') || updateVersion;

       var cmd = Aspectize.Host.PrepareCommand();

       cmd.Attributes.aasAsynchronousCall = true;
       cmd.Attributes.aasShowWaiting = true;
       cmd.Attributes.aasDataName = "MainData";
       cmd.Attributes.aasMergeData = true;

       if (shouldUpdateVersion) {
            session.SetField('Persist', true);
       }

       var That = this;

       cmd.OnComplete = function (result) {

           var em = Aspectize.EntityManagerFromContextDataName('MainData');

           var session = em.GetInstance('Session', { Id: sessionId });

           restoreResizable();

           if (shouldUpdateVersion) {

               That.UpdateUrl(session.CirculatingId);
           }

           That.DisplayResult(session.CirculatingId, session.Log);

       };

       cmd.Call('Server/DataService.SaveData', data, session.Id, session.CirculatingId, shouldUpdateVersion);

   },

   DisplayResult: function(id, log) {
       var uiService = Aspectize.Host.GetService("UIService");

       if (log) {
           uiService.ShowView("ErrorResult");
       } else {

           Aspectize.StartShowWaiting();

           var href = window.location.href;

           var parts = href.split('/');

           parts[parts.length - 2] += 'RT';

           var UrlRT = 'http://';

           for (var i = 2; i < parts.length - 1; i++) {
               UrlRT += parts[i] + '/';
           }

           UrlRT += "app.ashx?@Id=";
           UrlRT += id;
           //var url = "http://localhost/WebHost/PlayGroundRT/app.ashx?@Id=" + id;

           uiService.ShowView("IFrameResult");

           uiService.SetControlProperty("IFrameResult-IFrameApplication", "Url", UrlRT);

           Aspectize.StartShowWaiting();
       }

   },

   //SetIFrameUrl: function (iframe, id) {
   //    var uiService = Aspectize.Host.GetService("UIService");

   //    var url = "http://localhost/WebHost/PlayGroundRT/app.ashx?@Id=" + id;

   //    uiService.SetControlProperty(iframe, "Url", url);
   //},

   UpdateUrl: function (id) {

       if (id) {
           var href = window.location.href;

           var parts = href.split('/');

           var newUrl = 'http://';

           for (var i = 2; i < parts.length - 1; i++) {
               newUrl += parts[i] + '/';
           }

           newUrl += "id=";
           newUrl += id;

           var historyManager = Aspectize.Host.GetService('History');
           historyManager.PushState(null, null, id, null, newUrl);  //viewName, schemaPath, id, title, url
       }
   }

};

