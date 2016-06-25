/// <reference path="S:\Delivery\Aspectize.core\AspectizeIntellisense.js" />

Global.ClientService = {

   aasService:'ClientService',
   aasPublished: true,
   aasCommandAttributes: { GetSession: { CanExecuteOnStart: true } },
   MainData: 'MainData',

   InitWelcome: function() {
       $('.Welcome').fadeTo(3000, 0.6, function () {

       });
   },

   StartTour: function() {
       var tour = new Tour({
           storage: false,
           debug: true,
           steps: [
           {
               element: "#RootView-StartTour",
               title: "Welcome to Aspectize Playground",
               content: "",
               placement: 'bottom'
           },
           {
               element: "#HtmlPanel",
               title: "This is the html editor",
               content: "You may define controls with your custom html. <br /><br />A control is a div with aas-control attribute containing any custom html.",
               placement: 'bottom',
               onShown: function (tour) {
                   $('.Welcome').css({ 'opacity': '0.1' });
                   $('.HtmlPanel').css({ 'background-color': 'yellow' });
               },
               onHide: function (tour) {
                   $('.HtmlPanel').css({ 'background-color': 'transparent' });
               }
           },
           {
               element: ".ToolPanel",
               title: "Click on tool to add element in your control",
               content: "Aspectize provides complex and customisable controls as repeater, grid, treeview, upload, or checkboxlist.<br /> <br />You may add any external widget, such as BootstrapDatePicker, JQueryAutocomplete, TyniMCE or chart library, integration is easy.",
               placement: 'right',
               onShow: function (tour) {
                   $('.ToolPanel').css({ 'border': '1px solid yellow' });
               },
               onHide: function (tour) {
                   $('.ToolPanel').css({ 'border': 'none' });
               }
           },
           {
               element: "#SchemaDialog",
               title: "This is an example of a Data schema",
               content: "Aspectize provides a Visual Studio DSL to design Data of your Application<br /> <br />In this Playground, the AdventureWorks schema is automtically loaded, so you can experiment Data Binding.",
               placement: 'left',
               onShow: function (tour) {
                   Aspectize.Host.ExecuteCommand('UIService.ShowView', 'SchemaDialog');
                   $('#SchemaDialog').css({ 'border': '1px solid yellow' });
               },
               onHide: function (tour) {
                   Aspectize.Host.ExecuteCommand('UIService.UnactivateView', 'SchemaDialog');
               }
           },
           {
               element: ".JsPanel ",
               title: "This is the client command editor",
               content: "You may write any kind of custom javascript code.<br /> <br />Code is organized in Services and  Commands. Code is mainly procedural, with no complexity, such as Dom Manipulation. <br /> <br /><h4>No javascript expertise is required to build full-ajax Applications !</h4>",
               placement: 'bottom',
               onShow: function (tour) {
                   Aspectize.Host.ExecuteCommand('ClientService.TogglePanelAndResize', 'DisplayJS', 'JsPanel');
                   $('.JsPanel').css({ 'border': '1px solid yellow' });
               },
               onHide: function (tour) {
                   Aspectize.Host.ExecuteCommand('ClientService.TogglePanelAndResize', 'DisplayJS', 'JsPanel');
                   $('.JsPanel').css({ 'border': 'none' });
               }
           },
           {
               element: ".BindingsPanel",
               title: "This is the binding editor",
               content: "Configure your binding with consist on the dynamic links between Data, Controls and Services. <br /> <br />The configuration is not code, but written in javascript, with a powerfull intellisense in Visual Studio, to guide you through your work.",
               placement: 'top',
               onShow: function (tour) {
                   $('.BindingsPanel').css({ 'border': '1px solid yellow' });
               },
               onHide: function (tour) {
                   $('.BindingsPanel').css({ 'border': 'none' });
               }
           }
           ],
           onEnd: function (tour) {
               $('.Welcome').fadeOut();
           }
       });

       // Initialize the tour
       tour.init();

       // Start the tour
       tour.start(true);


   },

   ResizeAllEditors: function () {
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

               //$('[data-toggle="popover"]').popover('hide');
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

       var editorId = $('.' + panel + ' .AceEditor').attr('id');
       ace.edit(editorId).resize();
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

