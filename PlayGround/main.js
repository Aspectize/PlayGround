
function Main() {

    var mainView = Aspectize.Host.InitApplication();

    var urlArgs = Aspectize.Host.UrlArgs;

    if (urlArgs && urlArgs.StartingCommandName == "ClientService.GetSession") {
        //var x = urlArgs;
    } else {

        //Aspectize.Host.ExecuteCommand('ClientService.NewSession');

        var em = Aspectize.EntityManagerFromContextDataName('MainData');

        var newGuid = Aspectize.Host.ExecuteCommand('SystemServices.NewGuid');
        var id = newGuid.substring(1, 10);

        var session = em.CreateInstance('Session', { Id: id });

        session.SetField('CirculatingId', id);

        session.SetField('Html', "<!DOCTYPE html>\n<div aas:control='Test'>\n\n</div>");
        //session.SetField('Html', '<!DOCTYPE html>\n<div aas:control="Test">\n\t<a name="myLink">xxxx</a>\n</div>');

        session.SetField('Bindings', "var test = Aspectize.CreateView('MainView', aas.Controls.Test);");
        //session.SetField('Bindings', 'var test = Aspectize.CreateView("MainView", aas.Controls.Test);\n\ntest.myLink.click.BindCommand(aas.Services.Browser.SystemServices.Alert("coucou"));');
    }

    var cmd = Aspectize.Host.PrepareCommand();

    cmd.Attributes.aasAsynchronousCall = true;
    cmd.Attributes.aasShowWaiting = true;
    cmd.Attributes.aasDataName = this.MainData;
    cmd.Attributes.aasMergeData = true;

    cmd.OnComplete = function (result) {

    };

    cmd.Call('Server/DataService.LoadSamples');

    Aspectize.Host.ActivateViewByName('MainView');

    /* jsfiddle.net/QkZL8/15/ */

    var containerWidth = $(".FixedWidth").width();

    var initialWidth = (containerWidth / 3) - 10;

    $(".LeftPanel").width(initialWidth);
    $(".MiddlePanel").width(initialWidth);
    $(".RightPanel").width(initialWidth);

    var maxWidth;

    $(".TopPanel, .CenterPanel").resizable({
        handles: "s",
        minHeight: 120,
        stop: function () {
            Aspectize.Host.ExecuteCommand('ClientService.ResizeAllEditors');
        }
    });

    $(".LeftPanel, .MiddlePanel").resizable({
        handles: "e",
        maxWidth: $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width() - 120,
        minWidth: 120,
        start: function (e, ui) {
            maxWidth = $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width();
        },
        resize: function (event, ui) {
            var currentWidth = ui.size.width;
            $(this).width(currentWidth);
            $(this).nextAll('.TogglePanel').not('.hidden').first().width(maxWidth - currentWidth);
        },
        stop: function (e, ui) {
            var newMaxWidth = $(this).width() + $(this).nextAll('.TogglePanel').not('.hidden').first().width() - 120;
            $(this).resizable("option", "maxWidth", newMaxWidth);

            Aspectize.Host.ExecuteCommand('ClientService.ResizeAllEditors');
        }
    });


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

                var newGuid = Aspectize.Host.ExecuteCommand('SystemServices.NewGuid');
                var id = newGuid.substring(1, 10);

                var session = em.CreateInstance('Session', { Id: id });

                session.SetField('CirculatingId', id);

                session.SetField('Html', "<!DOCTYPE html>\n<div aas:control='Test'>\n\n</div>");

                session.SetField('Bindings', "var test = Aspectize.CreateView('MainView', aas.Controls.Test);");
            }
        }
    });

}

function OnApplicationEnd() {
    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    // Clean temp session
}
