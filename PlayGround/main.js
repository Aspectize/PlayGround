var maxWidth; var showResult;

function NewSession() {
    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    var newGuid = Aspectize.Host.ExecuteCommand('SystemServices.NewGuid');
    var id = newGuid.substring(1, 10);

    session = em.CreateInstance('Session', { Id: id });

    session.SetField('CirculatingId', id);

    session.SetField('Html', "<!DOCTYPE html>\n<div aas-control='Test'>\n\n</div>");
    //session.SetField('Html', '<!DOCTYPE html>\n<div aas:control="Test">\n\t<a name="myLink">xxxx</a>\n</div>');

    session.SetField('Bindings', "var test = Aspectize.CreateView('MainView', aas.Controls.Test);");

    session.SetField('js', "Global.MyService = {\n\n   aasService:'MyService',\n   aasPublished:true,\n\n   MyCommand: function () {\n\n   }\n};");

    return session;
}


function initResizablePanel(panel) {

    $(panel).resizable({
        handles: "e",
        maxWidth: $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width() - (50 * $('.TogglePanel').not('.hidden').length),
        minWidth: 120,
        start: function (e, ui) {
            maxWidth = $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width() - (50 * $('.TogglePanel').not('.hidden').length);
            //console.log("$(this).width(): " + $(this).width() + " $(this).nextAll('.TogglePanel').not('.hidden').first().width(): " + $(this).nextAll('.TogglePanel').not('.hidden').first().width() + " $('.TogglePanel').not('.hidden').length: " + $('.TogglePanel').not('.hidden').length + " sum: " + $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width() - (50 * $('.TogglePanel').not('.hidden').length));
        },
        resize: function (event, ui) {
            var currentWidth = ui.size.width;
            $(this).width(currentWidth);
            $(this).nextAll('.TogglePanel').not('.hidden').first().width(maxWidth - currentWidth + (50 * $('.TogglePanel').not('.hidden').length));
        },
        stop: function (e, ui) {
            var newMaxWidth = $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width() - (50 * $('.TogglePanel').not('.hidden').length);
            $(this).resizable("option", "maxWidth", newMaxWidth);

            Aspectize.Host.ExecuteCommand('ClientService.ResizeAllEditors');

            restoreResizable();
        }
    });
}

function initPanel(session) {
    var containerWidth = $(".FixedWidth").width();

    var nbPanelVisible = $('.TogglePanel').not('.hidden').length;

    var initialWidth = (containerWidth / nbPanelVisible) - 10 + 'px';

    var initialHeight = "248px";

    if (nbPanelVisible == 1) {
        $('.HtmlPanel').width(initialWidth);
        $('.CssPanel').width(initialWidth);
        $('.JsPanel').width(initialWidth);
    }  else if (session.HtmlWidth) {
        $('.HtmlPanel').width(session.HtmlWidth + 'px');
        $('.CssPanel').width(session.CSSWidth + 'px');
        $('.JsPanel').width(session.JsWidth + 'px');
    } else {
        $('.HtmlPanel').width(initialWidth);
        $('.CssPanel').width(initialWidth);
        $('.JsPanel').width(initialWidth);
    }

    if (session.BindingsHeight) {
        $('.BindingsPanel').height(session.BindingsHeight + 'px');
        $(".TopPanel").height(session.TopPanelHeight + 'px')
    } else {
        $('.BindingsPanel').height(initialHeight);
        $(".TopPanel").height(initialHeight)
    }

    if (nbPanelVisible == 2) {
        if ($(".HtmlPanel").hasClass('hidden')) {
            initResizablePanel(".CssPanel");
        } else {
            initResizablePanel(".HtmlPanel");
        }
    } else if (nbPanelVisible == 3) {
        initResizablePanel(".HtmlPanel, .CssPanel");
    }

}

function restoreResizable() {
    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    var session = em.GetAllInstances('Session')[0];

    session.SetField('HtmlWidth', $('.HtmlPanel').width());
    session.SetField('CSSWidth', $('.CssPanel').width());
    session.SetField('JsWidth', $('.JsPanel').width());
    session.SetField('BindingsHeight', $('.BindingsPanel').height());
    session.SetField('TopPanelHeight', $('.TopPanel').height());

    $('.ResizablePanel').each(function () {
        if (!$(this).hasClass("ui-resizable")) {
            $(this).addClass('ui-resizable');
        }
    });

}


function Main() {

    var mainView = Aspectize.Host.InitApplication();

    var urlArgs = Aspectize.Host.UrlArgs;

    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    var session;

    Aspectize.Host.ActivateViewByName('MainView');

    if (urlArgs && urlArgs.StartingCommandName == "ClientService.GetSession") {
        //session = em.GetAllInstances('Session')[0];
    } else {

        session = NewSession();

        //var newGuid = Aspectize.Host.ExecuteCommand('SystemServices.NewGuid');
        //var id = newGuid.substring(1, 10);

        //session = em.CreateInstance('Session', { Id: id });

        //session.SetField('CirculatingId', id);

        //session.SetField('Html', "<!DOCTYPE html>\n<div aas-control='Test'>\n\n</div>");
        ////session.SetField('Html', '<!DOCTYPE html>\n<div aas:control="Test">\n\t<a name="myLink">xxxx</a>\n</div>');

        //session.SetField('Bindings', "var test = Aspectize.CreateView('MainView', aas.Controls.Test);");
        //session.SetField('Bindings', 'var test = Aspectize.CreateView("MainView", aas.Controls.Test);\n\ntest.myLink.click.BindCommand(aas.Services.Browser.SystemServices.Alert("coucou"));');

        initPanel(session);
    }

    $(".TopPanel, .BindingsPanel").resizable({
        handles: "s",
        minHeight: 120,
        start: function (e, ui) {
            showResult = $('#IFrameResult').hasClass('aasActive');

            if (showResult) {
                Aspectize.Host.ExecuteCommand('UIService.ShowView', 'EmptyView');
            }
        },
        stop: function () {
            Aspectize.Host.ExecuteCommand('ClientService.ResizeAllEditors');
            restoreResizable();

            if (showResult) {
                Aspectize.Host.ExecuteCommand('UIService.ShowView', 'IFrameResult');
            }
        }
    });

    //initResizablePanel(".HtmlPanel, .CssPanel");

    $(".LinkControl").on('click', function () {
        Aspectize.Host.ExecuteCommand('ClientService.InsertControlDefinition', 'MainView-HTMLEditor', $(this).attr('id'));
    });



    Aspectize.InitializeHistoryManager(function (state) {

        if (state && state.Id) {
            Aspectize.Host.ExecuteCommand('ClientService.GetSession', state.Id);
        } else {
            if (urlArgs.Id) {
                Aspectize.Host.ExecuteCommand('ClientService.GetSession', urlArgs.Id);
            } else {
                var em = Aspectize.EntityManagerFromContextDataName('MainData');

                var sessions = em.GetAllInstances('Session');

                if (sessions.length > 0) {
                    em.ClearAllInstances('Session');
                }

                var session = NewSession();

                //var newGuid = Aspectize.Host.ExecuteCommand('SystemServices.NewGuid');
                //var id = newGuid.substring(1, 10);

                //var session = em.CreateInstance('Session', { Id: id });

                //session.SetField('CirculatingId', id);

                //session.SetField('Html', "<!DOCTYPE html>\n<div aas-control='Test'>\n\n</div>");

                //session.SetField('Bindings', "var test = Aspectize.CreateView('MainView', aas.Controls.Test);");
            }
        }
    });

}

function OnApplicationEnd() {
    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    // Clean temp session
}
