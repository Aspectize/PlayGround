/// <reference path="S:\Delivery\Aspectize.core\AspectizeIntellisenseLibrary.js" />

var maxWidth; var showResult;

function NewSession(firstLaunch) {
    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    var newGuid = Aspectize.Host.ExecuteCommand('SystemServices.NewGuid');
    var id = newGuid.substring(1, 10);

    session = em.CreateInstance('Session', { Id: id });

    session.SetField('CirculatingId', id);

    if (firstLaunch) {
        session.SetField('Html', "<!DOCTYPE html>\n<div aas-control='Test' class='container'>\n    <h1>My first view !</h1>\n  <div class='form-horizontal'> <div class='form-group'> <label class='control-label col-xs-2'>First Name:</label>\n   <div class='col-xs-2'>  <input type='text' name='TxtFirstName' class='form-control ' placeholder='Enter first name' />\n  </div>  <label class='control-label col-xs-2'>Last Name:</label>\n    <div class='col-xs-2'><input type='text' name='TxtLastName' class='form-control' placeholder='Enter last name' />\n    </div> </div>  </div><hr>\n    <h2>Hello {YourName} !</h2>\n</div>");
        var bindingsValue = "var test = Aspectize.CreateView('MyViewTest', aas"+".Controls.Test); \n test.YourName."+"BindData(aas" + ".Expression(test.TxtFirstName.value + ' ' + test.TxtLastName.value));";
        session.SetField('Bindings', bindingsValue);
        //session.SetField('Bindings', "var test = Aspectize.CreateView('MyViewTest', aas.Controls.Test); \n test.YourName.BindData(aas.Expression(test.TxtFirstName.value + ' ' + test.TxtLastName.value));");
    }
    else {
        session.SetField('Html', "<!DOCTYPE html>\n<div aas-control='Test'>\n    <h1>My first view !</h1>\n</div>");
        var bindingsValue = "var test = Aspectize.CreateView('MyViewTest', aas"+".Controls.Test);";
        session.SetField('Bindings', bindingsValue);
        //session.SetField('Bindings', "var test = Aspectize.CreateView('MyViewTest', aas.Controls.Test);");
    }

    session.SetField('js', "Global.MyService = {\n\n   aasService:'MyService',\n   MyCommand: function () {\n\n   }\n};");

    session.SetField('MainJS', "function Main() { \n    Aspectize.Host.InitApplication(); \n\n    Aspectize.Host.ExecuteCommand('UIService.ShowView', 'MyViewTest'); \n}");

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
        //console.log('initResizablePanel: ' + panel);
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
                //console.log('currentWidth: ' + currentWidth);

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

    var $items = $('.TogglePanel').not('.hidden');

    var nbPanelVisible = $items.length;

    var initialHeight = "248px";

    if (session.HtmlWidth) {
        $('.HtmlPanel').css({ width: session.HtmlWidth + '%'});
        $('.MainPanel').css({ width: session.MainWidth + '%' });
        $('.CssPanel').css({ width: session.CSSWidth + '%' });
        $('.JsPanel').css({ width: session.JsWidth + '%'});
    } else {
        var containerFullWidth = $(".TopPanel").width();

        var containerWidth = $(".TopPanel").width() - 18;

        var initialWidthPx = (containerWidth / nbPanelVisible);

        var initialWidth = (initialWidthPx / containerFullWidth) * 100;

        $items.each(function () {
            $(this).css({ width: initialWidth + '%' });
        });
    }

    if (session.BindingsHeight) {
        $('.BindingsPanel').height(session.BindingsHeight + 'px');
        $(".TopPanel").height(session.TopPanelHeight + 'px')
    } else {
        $('.BindingsPanel').height(initialHeight);
        $(".TopPanel").height(initialHeight)
    }

    $items.each(function () {
        if (!$(this).hasClass('hidden') && $(this).nextAll('.TogglePanel').not('.hidden').length > 0) {
            initResizablePanel($(this));
        }
    });

    Aspectize.Host.ExecuteCommand('ClientService.ResizeAllEditors');
}


function restoreResizable() {
    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    var session = em.GetAllInstances('Session')[0];

    session.SetField('HtmlWidth', $('.HtmlPanel').width() / $('.HtmlPanel').parent().width() * 100);
    session.SetField('MainWidth', $('.MainPanel').width() / $('.MainPanel').parent().width() * 100);
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

    //var urlArgs = Aspectize.Host.UrlArgs;
    var urlArgs = Aspectize.App.UrlArgs;    

    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    var session;

    Aspectize.Host.ExecuteCommand('UIService.ShowView', 'MainView');

    if (urlArgs && urlArgs.StartingCommandName == "ClientService.GetSession") {
        $('.Welcome').hide();
    } else {
        var firstLaunch = !Aspectize.Host.SessionManager.GetValue('TourFinished');
        session = NewSession(firstLaunch);

        initPanel(session);

        if (firstLaunch) {
            Aspectize.Host.ExecuteCommand('ClientService.InitWelcome');
        } else {
            $('.Welcome').hide();
        }
    }

    $('[data-toggle="popover"]').popover({ html: true, delay: { "show": 500, "hide": 100 } });

    //$('[data-toggle="popover"]').popover({
    //    placement: 'bottom',
    //    animation: true,
    //    trigger: 'manual', //<--- you need a trigger other than manual
    //    delay: {
    //        show: "500",
    //        hide: "100"
    //    }
    //});

    //setTimeout(function () {
    //    $('[data-toggle="popover"]').popover('show');
    //}, 1000);

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

    $(".LinkControl").on('click', function () {
        Aspectize.Host.ExecuteCommand('ClientService.InsertControlDefinition', 'HTMLEditor-Editor', $(this).attr('id'));
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
            }
        }
    });

    //Aspectize.Host.ExecuteCommand('ClientService.InitWelcome');
}

function OnApplicationEnd() {
    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    // Clean temp session
}
