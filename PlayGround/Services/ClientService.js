﻿/// <reference path="S:\Delivery\Aspectize.core\AspectizeIntellisense.js" />

Global.ClientService = {

   aasService:'ClientService',
   aasPublished: true,
   aasCommandAttributes: { GetSession: { CanExecuteOnStart: true } },
   MainData: 'MainData',
      
   GetSession: function (id) {

       if (id) {
           var cmd = Aspectize.Host.PrepareCommand();

           cmd.Attributes.aasAsynchronousCall = true;
           cmd.Attributes.aasShowWaiting = true;
           cmd.Attributes.aasDataName = this.MainData;
           cmd.Attributes.aasMergeData = true;

           var That = this;

           cmd.OnComplete = function (result) {
               //That.SetIFrameUrl('MainView-IFrameApplication', id);
               That.DisplayResult(id);
           };

           cmd.Call('Server/DataService.LoadSession', id);
       }
   },

   TogglePanel: function (buttonName, panelClass) {
       $('#' + buttonName).parent('li').toggleClass('active');

       $('.' + panelClass).toggleClass('hidden');
   },

   TogglePanelAndResize: function (buttonName, panelClass) {

       this.TogglePanel(buttonName, panelClass);
       var $items = $('.TogglePanel').not('.hidden');
       var nbVisible = $items.length;
       var totalWidth = $('.TopPanel').width();
       $items.each(function () {
           $(this).width((totalWidth / nbVisible) - 10);
       });
   },

   InsertControlDefinition: function (editorName, controlName) {
       var control = $('#' + controlName);

       var ctrlName = control.attr('data-aasName');
       var ctrlType = control.attr('data-aasType');
       var html = "<div aas:name='" + ctrlName + "' aas:type='" + ctrlType + "'></div>";

       Aspectize.Host.ExecuteCommand('AceClientService.InsertCode', editorName, html);
   },

   Run: function(data, sessionId, iframe, updateVersion) {
       var em = Aspectize.EntityManagerFromContextDataName('MainData');

       var session = em.GetInstance('Session', { Id: sessionId });

       var cmd = Aspectize.Host.PrepareCommand();

       cmd.Attributes.aasAsynchronousCall = true;
       cmd.Attributes.aasShowWaiting = true;

       var shouldUpdateVersion = (updateVersion == 'force') || updateVersion;

       //if (shouldUpdateVersion) {
       cmd.Attributes.aasDataName = "MainData";
           cmd.Attributes.aasMergeData = true;

        if (shouldUpdateVersion) {
             session.SetField('Persist', true);
       }

       var That = this;

       cmd.OnComplete = function (result) {

           if (shouldUpdateVersion) {

               var href = window.location.href;

               var parts = href.split('/');

               var newUrl = 'http://';

               for (var i = 2; i < parts.length - 1; i++) {
                   newUrl += parts[i] + '/';
               }

               newUrl += "id=";
               newUrl += result;

               var historyManager = Aspectize.Host.GetService('History');
               historyManager.PushState(null, null, null, null, newUrl);  //viewName, schemaPath, id, title, url

           }

           var em = Aspectize.EntityManagerFromContextDataName('MainData');

           var session = em.GetInstance('Session', { Id: sessionId });

           //session.SetField('CirculatingId', result);
           
           //That.SetIFrameUrl(iframe, session.CirculatingId);
           That.DisplayResult(session.CirculatingId);

       };

       cmd.Call('Server/DataService.SaveData', data, session.CirculatingId, shouldUpdateVersion);

   },

   DisplayResult: function(id) {
       var em = Aspectize.EntityManagerFromContextDataName('MainData');

       var session = em.GetInstance('Session', { Id: id });

       var uiService = Aspectize.Host.GetService("UIService");

       if (session.Log) {
           uiService.ShowView("ErrorResult");
       } else {

           var url = "http://localhost/WebHost/PlayGroundRT/app.ashx?@Id=" + id;

           uiService.ShowView("IFrameResult");

           uiService.SetControlProperty("IFrameResult-IFrameApplication", "Url", url);
       }

   },

   SetIFrameUrl: function (iframe, id) {
       var uiService = Aspectize.Host.GetService("UIService");

       var url = "http://localhost/WebHost/PlayGroundRT/app.ashx?@Id=" + id;

       uiService.SetControlProperty(iframe, "Url", url);
   }
};
