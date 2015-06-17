/// <reference path="S:\Delivery\Aspectize.core\AspectizeIntellisense.js" />

Global.ClientService = {

   aasService:'ClientService',
   aasPublished: true,
   aasCommandAttributes: { GetSession: { CanExecuteOnStart: true } },
   MainData: 'MainData',

   InitWelcome: function() {
       //Aspectize.Host.ExecuteCommand('UIService.SetModalView', 'HTMLEditor', true);
       //$('#aasModalWait').addClass('aasModalBackground');
       Aspectize.Host.ExecuteCommand('UIService.ShowView', 'Welcome');
       Aspectize.Host.ExecuteCommand('UIService.ShowView', 'MainView');
       $('#Welcome').addClass('aasActive').removeClass('aasNotActive');
   },

   ResizeAllEditors: function() {
       ace.edit('HTMLEditor-Editor').resize();
       ace.edit('MainEditor-Editor').resize();
       ace.edit('CSSEditor-Editor').resize();
       ace.edit('JSEditor-Editor').resize();
       ace.edit('BindingEditor-Editor').resize();
   },

   GetSession: function (id) {

       if (id) {
           var em = Aspectize.EntityManagerFromContextDataName(this.MainData);

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

               $('[data-toggle="popover"]').popover('hide');
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

       // si 1 seul panel visible, et c'est celui là qu'on cache: ne rien faire
       if (nbVisibleBefore == 1 && !$('.' + panel).hasClass('hidden')) return;

       // afficher le panel
       this.TogglePanel(name);

       var $items = $('.TogglePanel').not('.hidden');

       var nbVisible = $items.length;

       var containerFullWidth = $(".TopPanel").width();

       var containerWidth = $(".TopPanel").width() - 18;

       var initialWidthPx = (containerWidth / nbVisible);

       var initialWidth = (initialWidthPx / containerFullWidth) * 100;

       if (nbVisible == 1) initialWidth = 100;

       $items.each(function () {
           $(this).css({width: initialWidth + '%'});
       });

       restoreResizable();

       $items.each(function () {
           if (!$(this).hasClass('hidden') && $(this).nextAll('.TogglePanel').not('.hidden').length > 0) {
               initResizablePanel($(this));
           } else {
               destroyResizablePanel($(this));
           }
       });

       //if (nbVisible == 1) {
       //    destroyResizablePanel(".HtmlPanel");
       //    destroyResizablePanel(".MainPanel");
       //    destroyResizablePanel(".CssPanel");
       //} 

       //if (nbVisible == 1) {
       //    destroyResizablePanel(".HtmlPanel");
       //    destroyResizablePanel(".CssPanel");
       //} else if (nbVisible == 2) {
       //    if (panel == 'JsPanel') {
       //        destroyResizablePanel(".CssPanel");
       //    } else {
       //        initResizablePanel(".CssPanel");
       //    }
       //    initResizablePanel(".HtmlPanel");
       //} else if (nbVisible == 3) {
       //    initResizablePanel(".HtmlPanel");
       //    initResizablePanel(".CssPanel");
       //}

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

   BuildUrlRT: function(id) {
       var href = window.location.href;

       var parts = href.split('/');

       parts[parts.length - 2] += 'RT';

       var UrlRT = 'http://';

       for (var i = 2; i < parts.length - 1; i++) {
           UrlRT += parts[i] + '/';
       }

       UrlRT += "app.ashx?@Id=";
       UrlRT += id;

       return UrlRT;

   },

   DisplayResult: function(id, log) {
       var uiService = Aspectize.Host.GetService("UIService");

       if (log) {
           uiService.ShowView("ErrorResult");
       } else {

           Aspectize.StartShowWaiting();

           var UrlRT = this.BuildUrlRT(id);

           uiService.ShowView("IFrameResult");

           uiService.SetControlProperty("IFrameResult-IFrameApplication", "Url", UrlRT);

           Aspectize.StartShowWaiting();
       }

   },

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
   },

   InitDialogImage: function () {
       var uiService = Aspectize.Host.GetService("UIService");

       var activeImage = uiService.GetContextValue("ActiveImage") || 'Production';

       $('#SchemaImage > ul > li.' + activeImage).addClass('active');
       $('#SchemaImage > .tab-content > .tab-pane.' + activeImage).addClass('active');

       var windowTop = uiService.GetContextValue("WindowTop") || '57px';
       var windowLeft = uiService.GetContextValue("WindowLeft") || '278px';

       var windowHeight = uiService.GetContextValue("WindowHeight") || '493px';
       var windowWidth = uiService.GetContextValue("WindowWidth") || '923px';

       $('#SchemaDialog').css({top: windowTop, left: windowLeft, width:windowWidth, height:windowHeight});

   },

   CloseDialogImage: function () {
       var uiService = Aspectize.Host.GetService("UIService");

       uiService.SetContextValue("ActiveImage", $('#SchemaImage > ul > li.active > a').html());

       uiService.SetContextValue("WindowTop", $('#SchemaDialog').position().top);
       uiService.SetContextValue("WindowLeft", $('#SchemaDialog').position().left);
       uiService.SetContextValue("WindowHeight", $('#SchemaDialog').height());
       uiService.SetContextValue("WindowWidth", $('#SchemaDialog').width());

   }

};

