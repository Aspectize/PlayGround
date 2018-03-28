/// <reference path="S:\Delivery\Aspectize.core\AspectizeIntellisense.js" />

Global.ClientService = {

    aasService: 'ClientService',
    aasPublished: true,
    aasCommandAttributes: { GetSession: { CanExecuteOnStart: true } },
    MainData: 'MainData',

    InitWelcome: function () {

        
        $('.Welcome').fadeTo(3000, 0.6, function () {

        });
    },

    StartTour: function (sessionId) {
        var that = this;
        var tour = new Tour({
            storage: false,
            debug: true,
            onStart: function (tour) {
                $('#RootView-StartTour').hide();
            },
            template: "<div class='popover tour' style='min-width: 500px;'><div class='arrow'></div>  <h3 class='popover-title'></h3>  <div class='popover-content'></div>  <div class='popover-navigation'>    <button class='btn btn-default' data-role='prev'>« Prev</button>    <span data-role='separator'>|</span>    <button class='btn btn-default' data-role='next'>Next »</button>    <button class='btn btn-primary' data-role='end'>Got it !</button>  </div></div>",
            steps: [
            {
                element: "#title2",
                next: 1,
                prev: -1,
                title: "Welcome to Aspectize Playground<span class='pull-right'>1/7</span>",
                content: "Aspectize is a disruptive approach and technology to develop web and mobile App with Visual Studio. <br /> <br />Aspectize designed a total separation of different pieces of your App which ensures simplicity and clarity of design, a high productivity and a great reusability.<br /> <br /><h4>We garantee you will be amazed, how it is easy and robust.</h4>",
                placement: 'bottom'
            },
            {
                element: "#HtmlPanel",
                next: 2,
                prev: 0,
                title: "This is the html editor<span class='pull-right'>2/7</span>",
                content: "You define your UI with controls, not pages.<br /><br />A control is a standard div with aas-control attribute containing any custom html.<br /><br /><h4>Aspectize is in the Single Page Application category, all visible html is produced in your browser, without any single line of code.</h4>",
                placement: 'bottom',
                onShown: function (tour) {
                    $('.Welcome').css({ 'opacity': '0' });
                    $('.HtmlPanel').css({ 'background-color': 'red' });
                },
                onHide: function (tour) {
                    $('.HtmlPanel').css({ 'background-color': 'transparent' });
                }
            },
            {
                element: ".ToolPanel",
                next: 3,
                prev: 1,
                title: "Click on tool to add element in your control<span class='pull-right'>3/7</span>",
                content: "Aspectize provides standard and customisable controls such as repeater, grid, treeview, upload, or checkboxlist.<br /> <br />You may add any external widget, such as BootstrapDatePicker, JQueryAutocomplete, TyniMCE or chart library, integration is easy.",
                placement: 'right',
                onShow: function (tour) {
                    $('.ToolPanel').css({ 'border': '1px solid red' });
                },
                onHide: function (tour) {
                    $('.ToolPanel').css({ 'border': 'none' });
                }
            },
            {
                element: "#SchemaDialog",
                next: 4,
                prev: 2,
                title: "This is an example of a Data schema<span class='pull-right'>4/7</span>",
                content: "Data are designed with a Visual Studio DSL<br /> <br />All Data is produces by the server and have the same relationnal Model on the client and server side. <br /> <br />In this Playground, the AdventureWorks schema is automtically loaded, so you can experiment Data Binding.",
                placement: 'left',
                onShow: function (tour) {
                    Aspectize.Host.ExecuteCommand('UIService.ShowView', 'SchemaDialog');
                    $('#SchemaDialog').css({ 'border': '1px solid red' });
                },
                onHidden: function (tour) {
                    Aspectize.Host.ExecuteCommand('UIService.UnactivateView', 'SchemaDialog');
                }
            },
            {
                element: ".JsPanel ",
                next: 5,
                prev: 3,
                title: "This is the client command editor<span class='pull-right'>5/7</span>",
                content: "You may write any kind of custom javascript code.<br /> <br />Code is organized in Services and  Commands. Code is mainly procedural, with no complexity, such as Dom Manipulation. <br /> <br /><h4>No javascript expertise is required to build full-ajax Applications !</h4>",
                placement: 'bottom',
                onShow: function (tour) {
                    Aspectize.Host.ExecuteCommand('ClientService.TogglePanelAndResize', 'DisplayJS', 'JsPanel');
                    $('.JsPanel').css({ 'border': '1px solid red' });
                },
                onHide: function (tour) {
                    Aspectize.Host.ExecuteCommand('ClientService.TogglePanelAndResize', 'DisplayJS', 'JsPanel');
                    $('.JsPanel').css({ 'border': 'none' });
                }
            },
            {
                element: ".BindingsPanel",
                next: 6,
                prev: 4,
                title: "This is the binding editor<span class='pull-right'>6/7</span>",
                content: "Bindings are dynamic configuration over the Aspectize Engine, an unique controller<br /> <br />Configure your bindings wich consist on the dynamic links between Data, Controls and Services. <br /> <br />The configuration is not code, but written in javascript, with a powerfull intellisense in Visual Studio, to guide you through your work.",
                placement: 'top',
                onShow: function (tour) {
                    $('.BindingsPanel').css({ 'border': '1px solid red' });
                },
                onHide: function (tour) {
                    $('.BindingsPanel').css({ 'border': 'none' });
                }
            },
            {
                element: "#MainView-SelectSamples",
                next: -1,
                prev: 5,
                title: "Start browsing samples to discover how Aspectize works.<span class='pull-right'>7/7</span>",
                content: "Try to understand how the html, the data and the service, which totally separated, interacts together with the binding configuration.<br /> <br /><h4>Enjoy your trip to the next level of agility !</h4>",
                placement: 'top',
                onShow: function (tour) {
                    $('#MainView-SelectSamples').css({ 'border': '1px solid red' });
                },
                onHide: function (tour) {
                    $('#MainView-SelectSamples').css({ 'border': 'none' });
                }
            }
            ],
            onEnd: function (tour) {
                var em = Aspectize.EntityManagerFromContextDataName('MainData');
                var cmd = Aspectize.Host.PrepareCommand();

                cmd.Attributes.aasAsynchronousCall = true;
                //cmd.Attributes.aasShowWaiting = true;

                var That = this;

                cmd.OnComplete = function (result) {
                    
                }

                cmd.Call('ClientService.Run', em.GetDataSet(), sessionId);
                $('.Welcome').fadeOut();

                Aspectize.Host.ExecuteCommand('Server/DataService.TourFinished');
                $('.Welcome').hide();
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
            $(this).css({ width: initialWidth + '%' });
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

    Run: function (data, sessionId, updateVersion) {
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

    BuildUrlRT: function (id) {
        var href = window.location.href;

        var parts = href.split('/');

        parts[parts.length - 2] += 'RT';

        var UrlRT = parts[0] + '//';

        for (var i = 2; i < parts.length - 1; i++) {
            UrlRT += parts[i] + '/';
        }

        UrlRT += "app.ashx?@Id=";
        UrlRT += id;

        return UrlRT;

    },

    DisplayResult: function (id, log) {
        var uiService = Aspectize.Host.GetService("UIService");

        if (log) {
            uiService.ShowView("ErrorResult");
        } else {

            //Aspectize.StartShowWaiting();

            var UrlRT = this.BuildUrlRT(id);

            uiService.ShowView("IFrameResult");

            uiService.SetControlProperty("IFrameResult-IFrameApplication", "Url", UrlRT);

            //Aspectize.StopShowWaiting();
        }

    },

    UpdateUrl: function (id) {

        if (id) {
            var href = window.location.href;

            var parts = href.split('/');

            var newUrl = 'https://';

            for (var i = 2; i < parts.length - 1; i++) {
                newUrl += parts[i] + '/';
            }

            newUrl += "id=";
            newUrl += id;

            var historyManager = Aspectize.Host.GetService('History');
            historyManager.PushState('MainView', null, id, null, newUrl);  //viewName, schemaPath, id, title, url
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

        $('#SchemaDialog').css({ top: windowTop, left: windowLeft, width: windowWidth, height: windowHeight });

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

