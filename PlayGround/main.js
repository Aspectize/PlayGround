
function Main() {

    var mainView = Aspectize.Host.InitApplication();

    var urlArgs = Aspectize.Host.UrlArgs;

    if (urlArgs && urlArgs.StartingCommandName == "ClientService.GetSession") {
        //var x = urlArgs;
    } else {

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
        minHeight: 120
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
        }
    });


    $(".LinkControl").on('click', function () {
        Aspectize.Host.ExecuteCommand('ClientService.InsertControlDefinition', 'MainView-HTMLEditor', $(this).attr('id'));
    });


    Aspectize.InitializeHistoryManager(function (state) {
        var uiService = Aspectize.Host.GetService('UIService');

        if (state && state.url) {
            var x = state.url;
            window.location.href = state.url;
        } else {
            var currentUrl = window.location.href;

            if (urlArgs) {

            } else {

            }
            //window.location.href = ;
        }


    });


    //$(".MiddlePanel").resizable({
    //    handles: "e",
    //    maxWidth: $(".MiddlePanel").width() + $(".RightPanel").width() - 120,
    //    minWidth: 120,
    //    start: function (e, ui) {
    //        rightWidth = $(".MiddlePanel").width() + $(".RightPanel").width();
    //    },
    //    resize: function (event, ui) {

    //        var initialWidth = ui.originalSize.width;
    //        var currentWidth = ui.size.width;

    //        var padding = 10;

    //        $(this).width(currentWidth);

    //        // set the content panel width
    //        $(".RightPanel").width(rightWidth - currentWidth);

    //    },
    //    stop: function (e, ui) {
    //        var maxWidthMiddle = $(".MiddlePanel").width() + $(".RightPanel").width() - 120;
    //        $(".MiddlePanel").resizable("option", "maxWidth", maxWidthMiddle);
    //        var maxWidthLeft = $(".MiddlePanel").width() + $(".LeftPanel").width() - 120;
    //        $(".LeftPanel").resizable("option", "maxWidth", maxWidthLeft);
    //    }
    //});

    //$(".LeftPanel").resizable({
    //    handles: "e",
    //    maxWidth: $(".LeftPanel").width() + $(".MiddlePanel").width() - 120,
    //    minWidth: 120,
    //    start: function (e, ui) {
    //        leftWidth = $(".LeftPanel").width() + $(".MiddlePanel").width();
    //    },
    //    resize: function (event, ui) {

    //        var initialWidth = ui.originalSize.width;
    //        var currentWidth = ui.size.width;

    //        var total = $(this).width() + $(this).next().width();
    //        var total2 = $(".LeftPanel").width() + $(".MiddlePanel").width();
    //        console.log('currentWidth: ' + currentWidth + ' this.width: ' + $(this).width() + ' this.next.width: ' + $(this).next().width() + ' total: ' + total + ' total2: ' + total2);
    //        // this accounts for padding in the panels + 
    //        // borders, you could calculate this using jQuery
    //        var padding = 10;

    //        // this accounts for some lag in the ui.size value, if you take this away 
    //        // you'll get some instable behaviour
    //        $(this).width(currentWidth);

    //        // set the content panel width
    //        $(".MiddlePanel").width(leftWidth - currentWidth);
    //    },
    //    stop: function (e, ui) {
    //        var maxWidthMiddle = $(".MiddlePanel").width() + $(".RightPanel").width() - 120;
    //        $(".MiddlePanel").resizable("option", "maxWidth", maxWidthMiddle);
    //        var maxWidthLeft = $(".MiddlePanel").width() + $(".LeftPanel").width() - 120;
    //        $(".LeftPanel").resizable("option", "maxWidth", maxWidthLeft);
    //    }
    //});
}

function OnApplicationEnd() {
    var em = Aspectize.EntityManagerFromContextDataName('MainData');

    // Clean temp session
}
