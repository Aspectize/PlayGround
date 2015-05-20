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

function destroyResizablePanel(panel)
{
    var isResizable = ($(panel).children().length == 2);

    if (isResizable) {
        console.log('destroyResizablePanel: ' + panel);
        $(panel).resizable('destroy');
    }
}

function initResizablePanel(panel) {

    var isResizable = ($(panel).children().length == 2);

    if (!isResizable) {
        console.log('initResizablePanel: ' + panel);
        var constWidth;

        $(panel).resizable({
            handles: "e",
            maxWidth: $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width() - (30 * $('.TogglePanel').not('.hidden').length),
            minWidth: 100,
            start: function (e, ui) {
                var contentWidth = $('.TopPanel').outerWidth();
                maxWidth = $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width() - (30 * $('.TogglePanel').not('.hidden').length);
                constWidth = ($(this).outerWidth() + $(this).nextAll('.TogglePanel').not('.hidden').first().outerWidth());
            },
            resize: function (event, ui) {
                var currentWidth = ui.size.width;
                console.log('currentWidth: ' + currentWidth);

                var parent = ui.element.parent();
                var uiNext = ui.element.nextAll('.TogglePanel').not('.hidden').first();
                var w = constWidth - ui.element.outerWidth();
                // parent.outerWidth() * 100);
                uiNext.css({
                    width: w / parent.outerWidth() * 100 + "%"
                });
            },
            stop: function (e, ui) {
                var newMaxWidth = $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width() - (30 * $('.TogglePanel').not('.hidden').length);
                $(this).resizable("option", "maxWidth", newMaxWidth);

                Aspectize.Host.ExecuteCommand('ClientService.ResizeAllEditors');

                restoreResizable();

                var parent = ui.element.parent();
                ui.element.css({
                    width: (ui.element.outerWidth()) / parent.outerWidth() * 100 + "%"
                });

                var uiNext = ui.element.nextAll('.TogglePanel').not('.hidden').first();
                var parentNext = uiNext.parent();
                uiNext.css({
                    width: (uiNext.outerWidth()) / parentNext.outerWidth() * 100 + "%"
                });
            }
        });
    }
}

function initPanel(session) {

    var nbPanelVisible = $('.TogglePanel').not('.hidden').length;

    function initWidthRatio() {
        var containerFullWidth = $(".TopPanel").width();

        var containerWidth = $(".TopPanel").width() - 18;

        var initialWidthPx = (containerWidth / nbPanelVisible);

        //var initialWidth = (100 / nbPanelVisible) - 3 + '%';
        var initialWidth = (initialWidthPx / containerFullWidth) * 100;

        $('.HtmlPanel').css({ width: initialWidth + '%' });
        $('.CssPanel').css({ width: initialWidth + '%' });
        $('.JsPanel').css({ width: initialWidth + '%' });
    }

    var initialHeight = "248px";

    if (session.HtmlWidth) {
        $('.HtmlPanel').css({ width: session.HtmlWidth + '%'});
        $('.CssPanel').css({ width: session.CSSWidth + '%'});
        $('.JsPanel').css({ width: session.JsWidth + '%'});
    } else {
        initWidthRatio();
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
        initResizablePanel(".HtmlPanel");
        initResizablePanel(".CssPanel");
    }

    initWidthRatio();

    Aspectize.Host.ExecuteCommand('ClientService.ResizeAllEditors');
}


function restoreResizable() {
    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    var session = em.GetAllInstances('Session')[0];

    session.SetField('HtmlWidth', $('.HtmlPanel').width() / $('.HtmlPanel').parent().width() * 100);
    session.SetField('CSSWidth', $('.CssPanel').width() / $('.CssPanel').parent().width() * 100);
    session.SetField('JsWidth', $('.JsPanel').width() / $('.JsPanel').parent().width() * 100);
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
